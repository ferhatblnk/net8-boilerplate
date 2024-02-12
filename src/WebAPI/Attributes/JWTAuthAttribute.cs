using Business.Abstract;
using Core.Constants;
using Business.Utilities.Security.Jwt;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Attributes
{
    public class JWTAuthAttribute : TypeFilterAttribute
    {
        #region Ctor

        public JWTAuthAttribute() : base(typeof(AuthenticationFilter))
        {
        }

        #endregion

        #region Nested filter

        private class AuthenticationFilter : IAuthorizationFilter
        {
            JsonResult authorizationDenied
            {
                get
                {
                    var LS = ServiceTool.ServiceProvider.GetService<ILocalizationService>();

                    if (LS != null)
                        return new JsonResult(new ErrorResult(StatusCodes.Status401Unauthorized, LS.T(Messages.AuthorizationDenied)));
                    else
                        return new JsonResult(new ErrorResult(StatusCodes.Status401Unauthorized, Messages.AuthorizationDenied));
                }
            }

            #region Ctor

            public AuthenticationFilter()
            {
            }

            #endregion

            #region Methods

            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                try
                {
                    if (filterContext.Filters.Any(filter => filter is AuthenticationFilter))
                    {
                        if (filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues Authorization))
                        {
                            var token = Authorization.ToString().Replace("Bearer ", "");
                            var tokenHelper = ServiceTool.ServiceProvider.GetService<ITokenHelper>();

                            if (tokenHelper != null && tokenHelper.Validate(token))
                            {
                                var email = tokenHelper.GetClaim(token, ClaimNames.Email);

                                var key = $"{email}-{token}";
                                var _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();

                                if (_cacheManager == null || !_cacheManager.IsAdd(key))
                                {
                                    var userService = ServiceTool.ServiceProvider.GetService<IUserService>();

                                    if (userService != null)
                                    {
                                        var user = userService.GetUserByToken(token)?.Data;

                                        if (user == null || !user.TokenExpiredAt.HasValue || user.TokenExpiredAt.Value < DateTime.Now)
                                            filterContext.Result = authorizationDenied;
                                        else if (_cacheManager != null)
                                            _cacheManager.Add(key, true, 10);
                                    }
                                    else
                                        filterContext.Result = authorizationDenied;
                                }
                            }
                            else
                                filterContext.Result = authorizationDenied;
                        }
                        else
                            filterContext.Result = authorizationDenied;
                    }
                }
                catch
                {
                    filterContext.Result = authorizationDenied;
                }
            }

            #endregion
        }

        #endregion
    }
}
