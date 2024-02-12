using Core.Localization;
using Core.Utilities.IoC;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions
{
    public static class LocalizerExtensions
    {
        public static string ToStringLocalizer(this string input)
        {
            if (input == null)
                return "";

            try
            {
                var _localizer = ServiceTool.ServiceProvider.GetService<IStringLocalizer<Resource>>();

                return _localizer[input];
            }
            catch
            {
                return input;
            }
        }
    }
}
