using System.ComponentModel;

namespace Core.Constants
{
    public enum DataProvider
    {
        [Description("SqlServer")]
        SQLSERVER = 10,

        [Description("PostgreSql")]
        POSTGRESQL = 20
    }
}
