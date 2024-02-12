using Business.DependencyResolvers.Autofac;
using Business.Hubs.Concrete;
using Business.Utilities.Security.Jwt;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Settings.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt;
using DataAccess.DependencyResolvers;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebAPI.Filters;
using WebAPI.Infrastructure;
using HangfireJobs;
using Business.Hangfire.Managers.FireJobs;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
var tokenOptions = builder.Configuration.GetSection("TokenOptions");

builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.Configure<TokenOptions>(tokenOptions);

string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";

builder.Configuration.SetBasePath(Environment.CurrentDirectory);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

var appSettings = appSettingsSection.Get<AppSettings>();

//add localization
builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
builder.Services.AddLocalizationConfig();

//add swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            System.Array.Empty<string>()
        }
    });
});

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
    builder => builder.AllowAnyOrigin());
});

builder.Services.AddSignalR();

//add advanced dependency injection
builder.Services.AddAdvancedDependencyInjection();

builder.Services.AddDependencyResolvers(new ICoreModule[]
    {
        new CoreModule(),
        new DataModule(),
        new AutofacBusinessModule()
    });

builder.Services.AddHangfire(x =>
{
    x.UsePostgreSqlStorage(appSettings?.ConnectionString ?? "");
    //x.UseSqlServerStorage(appSettings?.ConnectionString ?? "");
});

builder.Services.AddHangfireServer();

builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

var app = builder.Build();

app.InitializeDatabase();

app.ConfigureCustomExceptionMiddleware();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseDefaultFiles();

var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
             "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//use localization
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (localizationOptions != null) app.UseRequestLocalization(localizationOptions.Value);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<HubService>("/notifier/2ec52d44-2750-40b4-9fa3-b49ea9262431");

var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var appSettingsProvider = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
var tokenHelper = serviceProvider.GetRequiredService<ITokenHelper>();

//use hangfire
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    IsReadOnlyFunc = (DashboardContext context) => false,
    Authorization = new[] { new HangfireAuthFilter(tokenHelper) }
});

//GlobalConfiguration.Configuration.UseSqlServerStorage(appSettingsProvider.Value.ConnectionString);
GlobalConfiguration.Configuration.UsePostgreSqlStorage(appSettingsProvider.Value.ConnectionString);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});

RecurringJobs.FetchUserOperation();
FireJobs.FireJob<FetchUserFireJobManager>(0);

app.Run();
