using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace WatchWord.EntityFrameworkCore
{
    public static class WatchWordDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<WatchWordDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<WatchWordDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
