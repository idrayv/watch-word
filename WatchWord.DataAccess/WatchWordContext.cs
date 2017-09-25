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

        /// <summary>Gets or sets subtitle files.</summary>
        public DbSet<SubtitleFile> SubtitleFiles { get; set; }

        /// <summary>Gets or sets word statistics.</summary>
        public DbSet<WordStatistic> WordStatistics { get; set; }

        /// <summary>Gets or sets favorite materials.</summary>
        public DbSet<FavoriteMaterial> FavoriteMaterials { get; set; }

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
                word.HasOne(w => w.Material).WithMany(m => m.Words).OnDelete(DeleteBehavior.Cascade).IsRequired();
            });

            modelBuilder.Entity<Composition>(composition =>
            {
                composition.ToTable("Compositions");
                composition.Property(c => c.Id).ValueGeneratedOnAdd();
                composition.HasOne(c => c.Word).WithMany(w => w.Compositions).OnDelete(DeleteBehavior.Cascade).IsRequired();
            });

            modelBuilder.Entity<Account>(account =>
            {
                account.ToTable("Accounts");
                account.Property(a => a.Id).ValueGeneratedOnAdd();
                account.HasIndex(a => a.ExternalId);
            });

            modelBuilder.Entity<Material>(material =>
            {
                material.ToTable("Materials");
                material.Property(m => m.Id).ValueGeneratedOnAdd();
#if MYSQL
                material.Property(m => m.Image).HasColumnType("TEXT").HasMaxLength(20000);
#endif
                material.Property(m => m.Name).HasMaxLength(256).IsRequired();
                material.Property(m => m.Image).IsRequired();
                material.Property(m => m.Description).HasMaxLength(1024).IsRequired();
                material.HasIndex(a => a.Type);
            });

            modelBuilder.Entity<VocabWord>(vocabWord =>
            {
                vocabWord.ToTable("VocabWords");
                vocabWord.Property(v => v.Id).ValueGeneratedOnAdd();
                vocabWord.HasOne(v => v.Owner).WithMany(a => a.VocabWords).OnDelete(DeleteBehavior.Cascade).IsRequired();
            });

            modelBuilder.Entity<Setting>(setting =>
            {
                setting.ToTable("Settings");
                setting.Property(s => s.Id).ValueGeneratedOnAdd();
                setting.HasIndex(s => s.Key);
                setting.HasOne(s => s.Owner).WithMany(a => a.Settings).OnDelete(DeleteBehavior.Cascade).IsRequired();
            });

            modelBuilder.Entity<Translation>(translation =>
            {
                translation.ToTable("Translations");
                translation.Property(t => t.Id).ValueGeneratedOnAdd();
                translation.Property(t => t.Translate).HasMaxLength(256).IsRequired();
                translation.Property(t => t.Word).HasMaxLength(256).IsRequired();
                translation.HasIndex(t => t.Word);
            });

            modelBuilder.Entity<SubtitleFile>(subtitleFile =>
            {
                subtitleFile.ToTable("SubtitleFiles");
                subtitleFile.Property(s => s.Id).ValueGeneratedOnAdd();
                subtitleFile.Property(s => s.SubtitleText).IsRequired();
                subtitleFile.HasOne(s => s.Material).WithMany(m => m.SubtitleFiles).OnDelete(DeleteBehavior.Cascade).IsRequired();
            });

            modelBuilder.Entity<WordStatistic>(wordStatistic =>
            {
                wordStatistic.ToTable("WordStatistics");
                wordStatistic.Property(w => w.Id).ValueGeneratedOnAdd();
                wordStatistic.Property(w => w.Word).IsRequired();
                wordStatistic.Property(w => w.TotalCount).IsRequired();
                wordStatistic.Property(w => w.KnownCount).IsRequired();
                wordStatistic.Property(w => w.LearnCount).IsRequired();
            });

            modelBuilder.Entity<FavoriteMaterial>(favoriteMaterial =>
            {
                favoriteMaterial.ToTable("FavoriteMaterials");
                favoriteMaterial.Property(f => f.Id).ValueGeneratedOnAdd();
                favoriteMaterial.HasOne(f => f.Material).WithMany(m => m.FavoriteMaterials).OnDelete(DeleteBehavior.Cascade).IsRequired();
                favoriteMaterial.HasOne(f => f.Account).WithMany(a => a.FavoriteMaterials).OnDelete(DeleteBehavior.Cascade).IsRequired();
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