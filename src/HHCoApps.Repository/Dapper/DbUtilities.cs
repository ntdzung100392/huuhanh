using System.Configuration;

namespace HHCoApps.Repository.Dapper
{
    internal class DbUtilities
    {
        internal static string GetConnString(string dbName)
        {
            return ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
        }
    }
}
