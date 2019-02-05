using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using WatchWord.Authorization.Roles;
using WatchWord.Authorization.Users;
using WatchWord.MultiTenancy;
using WatchWord.Entities;

namespace WatchWord.EntityFrameworkCore
{
    public class WatchWordDbContext : AbpZeroDbContext<Tenant, Role, User, WatchWordDbContext>
    {
        public WatchWordDbContext(DbContextOptions<WatchWordDbContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(120);
        }

        /// <summary>Gets or sets the words.</summary>
        public DbSet<Word> Words { get; set; }

        /// <summary>Gets or sets the compositions.</summary>
        public DbSet<Composition> Compositions { get; set; }

        /// <summary>Gets or sets the materials.</summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>Gets or sets the vocabulary words.</summary>
        public DbSet<VocabWord> VocabWords { get; set; }

        /// <summary>Gets or sets translations.</summary>
        public DbSet<Translation> Translations { get; set; }

        /// <summary>Gets or sets subtitle files.</summary>
        public DbSet<SubtitleFile> SubtitleFiles { get; set; }

        /// <summary>Gets or sets word statistics.</summary>
        public DbSet<WordStatistic> WordStatistics { get; set; }

        /// <summary>Gets or sets favorite materials.</summary>
        public DbSet<FavoriteMaterial> FavoriteMaterials { get; set; }

        /// <summary>Gets or sets vocab word statistics.</summary>
        public DbSet<VocabWordStatistic> VocabWordStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Material>(material =>
            {
                material.ToTable("Materials");
                material.Property(m => m.Id).ValueGeneratedOnAdd();

                material.Property(m => m.Image).HasColumnType("LONGTEXT").HasMaxLength(30000);

                material.Property(m => m.Name).HasMaxLength(256).IsRequired();
                material.Property(m => m.Image).IsRequired();
                material.Property(m => m.Description).HasMaxLength(1024).IsRequired();
                material.HasIndex(a => a.Type);
            });

            modelBuilder.Entity<VocabWord>(vocabWord =>
            {
                vocabWord.ToTable("VocabWords");
                vocabWord.Property(v => v.Id).ValueGeneratedOnAdd();
                vocabWord.HasOne(v => v.Owner).WithMany().OnDelete(DeleteBehavior.Cascade).IsRequired();
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

            modelBuilder.Entity<VocabWordStatistic>(vocabWordStatistic =>
            {
                vocabWordStatistic.ToTable("VocabWordStatistics");
                vocabWordStatistic.Property(w => w.Id).ValueGeneratedOnAdd();
                vocabWordStatistic.Property(w => w.Word).IsRequired();
                vocabWordStatistic.Property(w => w.CorrectGuesses).IsRequired();
                vocabWordStatistic.Property(w => w.WrongGuesses).IsRequired();
            });

            modelBuilder.Entity<FavoriteMaterial>(favoriteMaterial =>
            {
                favoriteMaterial.ToTable("FavoriteMaterials");
                favoriteMaterial.Property(f => f.Id).ValueGeneratedOnAdd();
                favoriteMaterial.HasOne(f => f.Material).WithMany(m => m.FavoriteMaterials).OnDelete(DeleteBehavior.Cascade).IsRequired();
                favoriteMaterial.HasOne(f => f.Account).WithMany().OnDelete(DeleteBehavior.Cascade).IsRequired();
            });

            modelBuilder.ForSqlServerUseIdentityColumns();
        }
    }
}
