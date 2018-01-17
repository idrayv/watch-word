using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using WatchWord.Authorization.Roles;
using WatchWord.Authorization.Users;
using WatchWord.MultiTenancy;

namespace WatchWord.EntityFrameworkCore
{
    public class WatchWordDbContext : AbpZeroDbContext<Tenant, Role, User, WatchWordDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public WatchWordDbContext(DbContextOptions<WatchWordDbContext> options)
            : base(options)
        {
        }
    }
}
