using Castle.DynamicProxy;
using Core.Constants;
using Core.DependencyInjection;
using Core.Settings.Concrete;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalizationConfig(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("tr-TR")
                };

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var languages = context.Request.Headers["Accept-Language"].ToString();
                    var currentLanguage = languages.Split(',').FirstOrDefault();
                    var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "en-US" : currentLanguage;

                    if (supportedCultures.Where(x => x.Name == defaultLanguage).Count() == 0)
                        defaultLanguage = "en-US";

                    return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
                }));
            });

            return services;
        }

        public static IServiceCollection AddAdvancedDependencyInjection(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblies(AssemblyContext.GetApplicationAssemblies())
            .AddClassesFromInterfaces());

            return services.AddCommonServices();
        }

        private static IImplementationTypeSelector AddClassesFromInterfaces(this IImplementationTypeSelector selector)
        {
            //singleton
            selector.AddClasses(classes => classes.AssignableTo<ISingletonLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithSingletonLifetime()

            .AddClasses(classes => classes.AssignableTo<ISelfSingletonLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
            .WithSingletonLifetime()

            //transient
            .AddClasses(classes => classes.AssignableTo<ITransientLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithTransientLifetime()

            .AddClasses(classes => classes.AssignableTo<ISelfTransientLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
            .WithTransientLifetime()

            //scoped
            .AddClasses(classes => classes.AssignableTo<IScopedLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo<ISelfScopedLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
            .WithScopedLifetime();

            return selector;
        }

        private static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.TryAddSingleton(services);

            return services;
        }

        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }

            return ServiceTool.Create(services);
        }

        public static DbContextOptionsBuilder BuildAppDbContext(this DbContextOptionsBuilder optionsBuilder, AppSettings appSettings)
        {
            if (string.IsNullOrWhiteSpace(appSettings.ConnectionString))
                throw new InvalidOperationException("Could not find a connection string.");

            switch (appSettings.DataProvider)
            {
                case DataProvider.SQLSERVER:
                {
                    optionsBuilder.UseSqlServer(appSettings.ConnectionString, o =>
                    {
                        o.MigrationsHistoryTable("_MigrationHistory");
                        o.MigrationsAssembly("Migrations.SQLServer");
                    });
                    break;
                }
                case DataProvider.POSTGRESQL:
                {
                    optionsBuilder.UseNpgsql(appSettings.ConnectionString, o =>
                    {
                        o.MigrationsHistoryTable("_MigrationHistory");
                        o.MigrationsAssembly("Migrations.PostgreSQL");
                    });
                    break;
                }
                default:
                {
                    throw new NotSupportedException($"{appSettings.DataProvider} provider doesn't support.");
                }
            }

            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();

            return optionsBuilder;
        }

        public static IServiceCollection AddInterceptedScoped<TInterface, TImplementation>(this IServiceCollection serviceCollection) where TInterface : class where TImplementation : class, TInterface
        {
            serviceCollection.AddScoped<TImplementation>();

            serviceCollection.AddScoped(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
                var implementation = serviceProvider.GetRequiredService<TImplementation>();

                var interceptors = GetInterceptors<TImplementation>(serviceProvider);

                var options = new ProxyGenerationOptions() { Selector = new InterceptorSelector<TImplementation>() };

                return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, options, interceptors);
            });

            return serviceCollection;
        }

        private static IInterceptor[] GetInterceptors<TImplementation>(IServiceProvider serviceProvider) where TImplementation : class
        {
            var classAttributes = typeof(TImplementation).GetCustomAttributes(typeof(AttributeBase), true).Cast<AttributeBase>();
            var methodAttributes = typeof(TImplementation).GetMethods().SelectMany(s => s.GetCustomAttributes(typeof(AttributeBase), true).Cast<AttributeBase>());

            var attributes = classAttributes.Union(methodAttributes).OrderBy(o => o.Priority);

            var interceptors = attributes.Select(f => serviceProvider.GetRequiredService(typeof(InterceptorBase<>).MakeGenericType(f.GetType()))).Cast<IInterceptor>();

            return interceptors.ToArray();
        }

        public class InterceptorSelector<TImplementation> : IInterceptorSelector where TImplementation : class
        {
            public IInterceptor[] SelectInterceptors(Type type, MethodInfo methodInfo, IInterceptor[] interceptors)
            {
                var classAttributes = type.GetTypeInfo().GetCustomAttributes(typeof(AttributeBase), true).Cast<AttributeBase>().ToList();

                var methodParameterTypes = methodInfo.GetParameters().Select(s => s.ParameterType).ToArray();
                var concreteMethod = typeof(TImplementation).GetMethod(methodInfo.Name, methodParameterTypes);
                if (concreteMethod is not null)
                {
                    var methodAttributes = concreteMethod.GetCustomAttributes<AttributeBase>(true).Cast<AttributeBase>();
                    classAttributes.AddRange(methodAttributes);
                }

                var interceptorList = new List<IInterceptor>();
                foreach (var item in classAttributes.OrderBy(o => o.Priority))
                {
                    var baseType = typeof(InterceptorBase<>).MakeGenericType(item.GetType());

                    var interceptor = interceptors.FirstOrDefault(a => a.GetType().IsAssignableTo(baseType));
                    if (interceptor is not null)
                        interceptorList.Add(interceptor);
                }
                return interceptorList.ToArray();
            }
        }
    }
}
