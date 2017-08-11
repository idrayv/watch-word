using System;
using Microsoft.Extensions.Configuration;

namespace WatchWord.DataAccess
{
    public class DatabaseSettings
    {
        public readonly string ConnectionString;
        public readonly bool UseMySql;

        protected DatabaseSettings()
        {
        }

        public DatabaseSettings(IConfiguration configuration)
        {
            UseMySql = configuration["DatabaseSettings:MySql"] == "True";
            if (UseMySql)
            {
                ConnectionString = configuration["DatabaseSettings:ConnectionStringMySql"];
            }
            else
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
        }
    }
}