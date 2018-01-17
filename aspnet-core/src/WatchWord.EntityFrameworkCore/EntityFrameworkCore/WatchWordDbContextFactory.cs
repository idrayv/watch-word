using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WatchWord.Configuration;
using WatchWord.Web;

namespace WatchWord.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class WatchWordDbContextFactory : IDesignTimeDbContextFactory<WatchWordDbContext>
    {
        public WatchWordDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WatchWordDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            WatchWordDbContextConfigurer.Configure(builder, configuration.GetConnectionString(WatchWordConsts.ConnectionStringName));

            return new WatchWordDbContext(builder.Options);
        }
    }
}
