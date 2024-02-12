using System.ComponentModel;

namespace Core.Utilities.Security.Jwt
{
    public enum ClaimNames
    {
        [Description("Id")]
        Id = 10,

        [Description("Email")]
        Email = 20,

        [Description("Roles")]
        Roles = 30,

        [Description("StoreId")]
        StoreId = 40,

        [Description("Theme")]
        Theme = 50,

        [Description("Subdomain")]
        Subdomain = 60
    }
}
