using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MySQL.Data.Entity.Extensions;
using WatchWord.Domain.Entity;
using WatchWord.Domain.Identity;

namespace WatchWord.DataAccess
{
    public sealed class WatchWordContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
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

        // ReSharper disable once UnusedMember.Local
        private WatchWordContext() { }

        public WatchWordContext(DatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_dbSettings.UseMySql)
            {
                optionsBuilder.UseMySQL(_dbSettings.ConnectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
            }
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
                if (_dbSettings.UseMySql)
                {
                    material.Property(m => m.Image).HasColumnType("TEXT").HasMaxLength(20000);
                }
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
    }
}