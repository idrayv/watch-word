using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using WatchWord.Domain.Entity;
using WatchWord.DataAccess.Identity;

namespace WatchWord.DataAccess
{
    public sealed class WatchWordContext : IdentityDbContext<WatchWordUser, WatchWordRole, int>, IDesignTimeDbContextFactory<WatchWordContext>
    {
        private readonly DatabaseSettings _dbSettings;

        /// <summary>Gets or sets the words.</summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>Gets or sets the compositions.</summary>
        public DbSet<Composition> Compositions { get; set; }

        /// <summary>Gets or sets the accounts.</summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>Gets or sets the materials.</summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>Gets or sets the vocabulary words.</summary>
        public DbSet<VocabWord> VocabWords { get; set; }

        /// <summary>Gets or sets user's or site's settings.</summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>Gets or sets translations.</summary>
        public DbSet<Translation> Translations { get; set; }

        public WatchWordContext() { }

        public WatchWordContext(DatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
            Database.Migrate();
            Database.SetCommandTimeout(120);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if MYSQL
            optionsBuilder.UseMySql(_dbSettings.ConnectionString);
#elif !MYSQL
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
#if MYSQL
                material.Property(m => m.Image).HasColumnType("TEXT").HasMaxLength(20000);
#endif
                material.Property(m => m.Description);
            });

            modelBuilder.Entity<VocabWord>(knownWord =>
            {
                knownWord.ToTable("VocabWords");
                knownWord.Property(v => v.Id).ValueGeneratedOnAdd();
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

        public WatchWordContext CreateDbContext(string[] args)
        {
            var mockConfiguration = new CreateDbConfiguration
            {
#if MYSQL
                ConnectionString = "server=localhost;user id=root;password=password;database=WatchWord;Port=3306;"
#elif !MYSQL
                ConnectionString = "Server=M-SHCHYHOL\\SQLEXPRESS;Database=WatchWord;Integrated Security=SSPI;MultipleActiveResultSets=true"
#endif
            };
            var dbSettings = new DatabaseSettings(mockConfiguration);

            return new WatchWordContext(dbSettings);
        }

        private class CreateDbConfiguration : IConfiguration
        {
            public string ConnectionString;

            public string this[string key]
            {
                get
                {
#if MYSQL
                    if (key == "DatabaseSettings:ConnectionStringMySql")
#elif !MYSQL
                    if (key == "DatabaseSettings:ConnectionStringIdrayv")
#endif
                    {
                        return ConnectionString;
                    }
                    return "";
                }
                set => throw new System.NotImplementedException();
            }

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new System.NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new System.NotImplementedException();
            }

            public IConfigurationSection GetSection(string key)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}