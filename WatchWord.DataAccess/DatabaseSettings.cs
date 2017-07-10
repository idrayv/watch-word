using System;
using Microsoft.Extensions.Configuration;

namespace WatchWord.DataAccess
{
    public class DatabaseSettings
    {
        public readonly string ConnectionString;
        public readonly bool UseMySQL;

        protected DatabaseSettings() { }

        public DatabaseSettings(IConfiguration configuration)
        {
            UseMySQL = configuration["DatabaseSettings:MySql"] == "True";
            if (UseMySQL)
            {
                ConnectionString = configuration["DatabaseSettings:ConnectionStringMySql"];
            }
            else if (Environment.MachineName == "DESKTOP-Q21PF7P")
            {
                ConnectionString = configuration["DatabaseSettings:ConnectionStringTommyNotebook"];
            }
            else
            {
                ConnectionString = configuration["DatabaseSettings:ConnectionString"];
            }
        }
    }
}