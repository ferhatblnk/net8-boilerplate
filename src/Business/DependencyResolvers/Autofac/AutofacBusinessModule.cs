using Business.Abstract;
using Business.Abstract.Department;
using Business.Abstract.Role;
using Business.Abstract.UserAddress;
using Business.Abstract.UserDepartment;
using Business.BusinessAspects.Autofac;
using Business.Concrete;
using Business.Concrete.Department;
using Business.Concrete.Role;
using Business.Concrete.UserAddress;
using Business.Concrete.UserDepartment;
using Business.Utilities.Security.Jwt;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Db.Abstract;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.Mapping.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : System.Reflection.Module, ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<IProxyGenerator, ProxyGenerator>();

            //Generic Repository
            services.AddDbContext<AppDataContext>(contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Singleton);
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            //Helpers
            services.AddSingleton(typeof(ITokenHelper), typeof(JwtHelper));
            services.AddSingleton(typeof(ILog4DbService), typeof(Log4DbManager));

            //Interceptors
            services.AddTransient<InterceptorBase<CacheAttribute>, CacheInterceptor>();
            services.AddTransient<InterceptorBase<DailyCacheAttribute>, DailyCacheInterceptor>();
            services.AddTransient<InterceptorBase<MonthlyCacheAttribute>, MonthlyCacheInterceptor>();
            services.AddTransient<InterceptorBase<YearlyCacheAttribute>, YearlyCacheInterceptor>();
            services.AddTransient<InterceptorBase<CacheRemoveAttribute>, CacheRemoveInterceptor>();
            services.AddTransient<InterceptorBase<ExceptionLogAttribute>, ExceptionLogInterceptor>();
            services.AddTransient<InterceptorBase<Log4RequestAttribute>, Log4RequestInterceptor>();
            services.AddTransient<InterceptorBase<Log4ResponseAttribute>, Log4ResponseInterceptor>();
            services.AddTransient<InterceptorBase<PerformanceAttribute>, PerformanceInterceptor>();
            services.AddTransient<InterceptorBase<TransactionAttribute>, TransactionInterceptor>();
            services.AddTransient<InterceptorBase<ValidationAttribute>, ValidationInterceptor>();
            services.AddTransient<InterceptorBase<SecuredOperationAttribute>, SecuredOperationInterceptor>();

            //Services
            services.AddInterceptedScoped<ILocalizationService, LocalizationManager>();
            services.AddInterceptedScoped<IAuthService, AuthManager>();
            services.AddInterceptedScoped<IFileService, FileManager>();
            services.AddInterceptedScoped<ISignalRService, SignalRManager>();
            services.AddInterceptedScoped<ILanguageService, LanguageManager>();
            services.AddInterceptedScoped<ILocalizedPropertyService, LocalizedPropertyManager>();

            services.AddInterceptedScoped<IUserService, UserManager>();
            services.AddInterceptedScoped<IDepartmentService, DepartmentManager>();
            services.AddInterceptedScoped<IRoleService, RoleManager>();
            services.AddInterceptedScoped<IUserRoleService, UserRoleManager>();

            services.AddInterceptedScoped<IUserAddressService, UserAddressManager>();
            services.AddInterceptedScoped<IUserDepartmentService, UserDepartmentManager>();


        }
    }
}
