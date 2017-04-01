using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess
{
    class WatchWordContext : DbContext
    {
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

        public WatchWordContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DRAYV-PC\SQLEXPRESS;Database=WatchWord;Integrated Security=SSPI;MultipleActiveResultSets=true",
                options => options.EnableRetryOnFailure());
        }
    }
}