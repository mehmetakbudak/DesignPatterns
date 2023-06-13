using Udemy.Strategy.Enums;

namespace Udemy.Strategy.Models
{
    public class Settings
    {
        public static string ClaimDatabaseType = "databasetype";
        public DatabaseType DatabaseType;
        public DatabaseType GetDefaultDatabaseType => DatabaseType.SqlServer;
    }
}
