using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WatchWord.Domain.Entity;
using WatchWord.Domain.Identity;

namespace WatchWord.DataAccess
{
    public class WatchWordContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        private readonly IConfiguration _configuration;

        /// <summary>Gets or sets the words.</summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>Gets or sets the compositions.</summary>
        public DbSet<Composition> Compositions { get; set; }

        /// <summary>Gets or sets the accounts.</summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>Gets or sets the materials.</summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>Gets or sets the vocabulary of known words.</summary>
        public DbSet<KnownWord> KnownWords { get; set; }

        /// <summary>Gets or sets the vocabulary of learning words.</summary>
        public DbSet<LearnWord> LearnWords { get; set; }

        /// <summary>Gets or sets user's or site's settings.</summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>Gets or sets translations.</summary>
        public DbSet<Translation> Translations { get; set; }

        protected WatchWordContext()
        {
        }

        public WatchWordContext(IConfiguration configuration)
        {
            _configuration = configuration;

            Init();
        }

        private void Init()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["DatabaseSettings:ConnectionString"]);
        }
    }
}