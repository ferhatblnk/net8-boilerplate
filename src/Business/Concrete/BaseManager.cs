using Business.Abstract;
using Business.Utilities.Security.Jwt;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities;
using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using Business.Hubs.Abstract;
using Business.Hubs.Concrete;
using System;
using System.Linq;
using Core.Constants;
using Core.Extensions;
using DataAccess.Concrete.Mapping.Membership;
using Entities.Concrete.Membership;
using Entities.Concrete.UserAddress;

namespace Business.Concrete
{
    public class BaseManager
    {
        #region Global Variables

        ILanguageService _cacheLanguageService;
        ILocalizedPropertyService _cacheLocalizedPropertyService;
        ILocalizationService _cacheLocalizationService;

        IFileService _cacheFileService;
        IHubContext<HubService, IHubService> _cacheHubService;
        IHttpContextAccessor _cacheHttpContext;
        ITokenHelper _cacheTokenHelper;

        protected ILanguageService _languageService => _cacheLanguageService ??= ServiceTool.ServiceProvider.GetService<ILanguageService>();
        protected ILocalizationService LS => _cacheLocalizationService ??= ServiceTool.ServiceProvider.GetService<ILocalizationService>();
        protected ILocalizedPropertyService LP => _cacheLocalizedPropertyService ??= ServiceTool.ServiceProvider.GetService<ILocalizedPropertyService>();

        protected IFileService _fileService => _cacheFileService ??= ServiceTool.ServiceProvider.GetService<IFileService>();
        protected IHubContext<HubService, IHubService> _hubService => _cacheHubService ??= ServiceTool.ServiceProvider.GetService<IHubContext<HubService, IHubService>>();
        protected IHttpContextAccessor _httpContext => _cacheHttpContext ??= ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        protected ITokenHelper _tokenHelper => _cacheTokenHelper ??= ServiceTool.ServiceProvider.GetService<ITokenHelper>();

        private Guid GuidFromToken(ClaimNames claimType)
        {
            try
            {
                if (_tokenHelper.Validate(_currentToken))
                    return new Guid(_tokenHelper.GetClaim(_currentToken, claimType));
            }
            catch { }

            return Guid.Empty;
        }

        private string GetRequestToken()
        {
            try
            {
                var authHeader = _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault() ?? "";

                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    return authHeader.Replace("Bearer ", "");
            }
            catch { }

            return string.Empty;
        }
        private string GetRequestLanguage()
        {
            try
            {
                return _httpContext.HttpContext.Request.Headers["lng"].FirstOrDefault() ?? Languages.Turkish.Code();
            }
            catch { }

            return Languages.Turkish.Code();
        }

        protected Guid _currentUserId => GuidFromToken(ClaimNames.Id);
        protected string _currentToken => GetRequestToken();
        protected TUser _currentUser => _userDal.Get(_currentUserId);
        protected TLanguage _currentLanguage
        {
            get
            {
                var code = "";

                try
                {
                    code = GetRequestLanguage();
                }
                catch { }

                return _languageDal.Get(x => x.LanguageCode == code) ?? _languageDal.Get(x => x.LanguageCode == Languages.Turkish.Code());
            }
        }

        #endregion

        #region Data Variables

        protected IRepository<TLanguage> _languageDal => ServiceTool.ServiceProvider.GetService<IRepository<TLanguage>>();
        protected IRepository<TLocalizedMap> _localizedMapDal => ServiceTool.ServiceProvider.GetService<IRepository<TLocalizedMap>>();
        protected IRepository<TLocalizedProperty> _localizedPropertyDal => ServiceTool.ServiceProvider.GetService<IRepository<TLocalizedProperty>>();
        protected IRepository<TSystemLog> _systemLogDal => ServiceTool.ServiceProvider.GetService<IRepository<TSystemLog>>();
        protected IRepository<TSystemSetting> _systemSettingDal => ServiceTool.ServiceProvider.GetService<IRepository<TSystemSetting>>();
        
        protected IRepository<TUser> _userDal => ServiceTool.ServiceProvider.GetService<IRepository<TUser>>();
        protected IRepository<TDepartment> _departmentDal => ServiceTool.ServiceProvider.GetService<IRepository<TDepartment>>();
        protected IRepository<TUserDepartment> _userDepartmentDal => ServiceTool.ServiceProvider.GetService<IRepository<TUserDepartment>>();
        protected IRepository<TRole> _roleDal => ServiceTool.ServiceProvider.GetService<IRepository<TRole>>();
        protected IRepository<TUserRole> _userRoleDal => ServiceTool.ServiceProvider.GetService<IRepository<TUserRole>>();
        protected IRepository<TUserAddress> _userAddressDal => ServiceTool.ServiceProvider.GetService<IRepository<TUserAddress>>();
        protected IRepository<TUserSetting> _userSettingDal => ServiceTool.ServiceProvider.GetService<IRepository<TUserSetting>>();

        #endregion
    }
}
