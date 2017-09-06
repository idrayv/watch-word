using System;
using Microsoft.Extensions.Configuration;

namespace WatchWord.DataAccess
{
    public class DatabaseSettings
    {
        public readonly string ConnectionString;

        protected DatabaseSettings()
        {
        }

        public DatabaseSettings(IConfiguration configuration)
        {
#if MYSQL
            ConnectionString = configuration["DatabaseSettings:ConnectionStringMySql"];
#elif !MYSQL
            switch (Environment.MachineName)
            {
                case "DESKTOP-Q21PF7P":
                    ConnectionString = configuration["DatabaseSettings:ConnectionStringTommyNotebook"];
                    break;
                case "M-SHCHYHOL":
                    ConnectionString = configuration["DatabaseSettings:ConnectionStringIdrayv"];
                    break;
                default:
                    ConnectionString = configuration["DatabaseSettings:ConnectionString"];
                    break;
            }
#endif
        }
    }
}