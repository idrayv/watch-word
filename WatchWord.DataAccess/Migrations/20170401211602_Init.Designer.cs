using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WatchWord.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Migrations
{
    [DbContext(typeof(WatchWordContext))]
    [Migration("20170401211602_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WatchWord.Domain.Entity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExternalId");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Composition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Line");

                    b.Property<int?>("WordId");

                    b.Property<int>("Сolumn");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("Compositions");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.KnownWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Translation");

                    b.Property<int>("Type");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("KnownWords");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.LearnWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Translation");

                    b.Property<int>("Type");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("LearnWords");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<string>("MimeType");

                    b.Property<string>("Name");

                    b.Property<int?>("OwnerId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Boolean");

                    b.Property<DateTime?>("Date");

                    b.Property<int>("Int");

                    b.Property<int>("Key");

                    b.Property<int?>("OwnerId");

                    b.Property<string>("String");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Translation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<int>("Source");

                    b.Property<string>("Translate");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int?>("MaterialId");

                    b.Property<string>("TheWord");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Composition", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Word", "Word")
                        .WithMany()
                        .HasForeignKey("WordId");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.KnownWord", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.LearnWord", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Material", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Setting", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Word", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Material", "Material")
                        .WithMany("Words")
                        .HasForeignKey("MaterialId");
                });
        }
    }
}
