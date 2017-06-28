using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MySQL.Data.Entity.Extensions;
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
            var isMySql = _configuration["DatabaseSettings:MySql"] == "True";
            if (isMySql)
            {
                optionsBuilder.UseMySQL(_configuration["DatabaseSettings:ConnectionStringMySql"]);
            }
            else
            {
                optionsBuilder.UseSqlServer(_configuration["DatabaseSettings:ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var isMySql = _configuration["DatabaseSettings:MySql"] == "True";

            modelBuilder.Entity<Word>(word =>
            {
                word.Property(w => w.Id).ValueGeneratedOnAdd();
                word.ToTable("Words");
                word.HasOne(w => w.Material).WithMany(m => m.Words).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Composition>(composition =>
            {
                composition.ToTable("Compositions");
                composition.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Account>(account =>
            {
                account.ToTable("Accounts");
                account.Property(a => a.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Material>(material =>
            {
                material.ToTable("Materials");
                material.Property(m => m.Id).ValueGeneratedOnAdd();
                if (isMySql)
                {
                    material.Property(m => m.Image).HasColumnType("TEXT").HasMaxLength(20000);
                }
                material.Property(m => m.Description);
            });

            modelBuilder.Entity<KnownWord>(knownWord =>
            {
                knownWord.ToTable("KnownWords");
                knownWord.Property(k => k.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<LearnWord>(learnWord =>
            {
                learnWord.ToTable("LearnWords");
                learnWord.Property(l => l.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Setting>(setting =>
            {
                setting.ToTable("Settings");
                setting.Property(s => s.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Translation>(translation =>
            {
                translation.ToTable("Translations");
                translation.Property(t => t.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.ForSqlServerUseIdentityColumns();
            base.OnModelCreating(modelBuilder);
        }
    }
}