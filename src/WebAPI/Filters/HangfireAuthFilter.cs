using Business.Utilities.Security.Jwt;
using Hangfire.Dashboard;

namespace WebAPI.Filters
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        private readonly ITokenHelper _tokenHelper;

        public HangfireAuthFilter(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        public bool Authorize(DashboardContext context)
        {
            var cookies = context.GetHttpContext().Request.Cookies;

            if (cookies["token"] != null)
            {
                var jwtToken = cookies["token"];

                return _tokenHelper.Validate(jwtToken);
            }

            return false;
        }
    }
}
