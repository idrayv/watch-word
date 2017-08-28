#if MYSQL

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchWord.DataAccess.Migrations
{
    [DbContext(typeof(WatchWordContext))]
    [Migration("20170828105454_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WatchWord.DataAccess.Identity.WatchWordRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WatchWord.DataAccess.Identity.WatchWordUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

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

                    b.Property<int>("Column");

                    b.Property<int>("Line");

                    b.Property<int?>("WordId");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("Compositions");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20000);

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

            modelBuilder.Entity("WatchWord.Domain.Entity.VocabWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Translation");

                    b.Property<int>("Type");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("VocabWords");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("WatchWord.DataAccess.Identity.WatchWordRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WatchWord.DataAccess.Identity.WatchWordUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WatchWord.DataAccess.Identity.WatchWordUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("WatchWord.DataAccess.Identity.WatchWordRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WatchWord.DataAccess.Identity.WatchWordUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WatchWord.DataAccess.Identity.WatchWordUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Composition", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Word", "Word")
                        .WithMany()
                        .HasForeignKey("WordId");
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

            modelBuilder.Entity("WatchWord.Domain.Entity.VocabWord", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("WatchWord.Domain.Entity.Word", b =>
                {
                    b.HasOne("WatchWord.Domain.Entity.Material", "Material")
                        .WithMany("Words")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

#elif !MYSQL

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchWord.DataAccess.Migrations
{
    [DbContext(typeof(WatchWordContext))]
    [Migration("20170828105454_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<int>("RoleId");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<int>("UserId");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
            {
                b.Property<string>("LoginProvider");

                b.Property<string>("ProviderKey");

                b.Property<string>("ProviderDisplayName");

                b.Property<int>("UserId");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
            {
                b.Property<int>("UserId");

                b.Property<int>("RoleId");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
            {
                b.Property<int>("UserId");

                b.Property<string>("LoginProvider");

                b.Property<string>("Name");

                b.Property<string>("Value");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens");
            });

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

                b.Property<int>("Column");

                b.Property<int>("Line");

                b.Property<int?>("WordId");

                b.HasKey("Id");

                b.HasIndex("WordId");

                b.ToTable("Compositions");
            });

            modelBuilder.Entity("WatchWord.Domain.Entity.Material", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Description");

                b.Property<string>("Image");

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

            modelBuilder.Entity("WatchWord.Domain.Entity.VocabWord", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int?>("OwnerId");

                b.Property<string>("Translation");

                b.Property<int>("Type");

                b.Property<string>("Word");

                b.HasKey("Id");

                b.HasIndex("OwnerId");

                b.ToTable("VocabWords");
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

            modelBuilder.Entity("WatchWord.Domain.Identity.ApplicationRole", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Name")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles");
            });

            modelBuilder.Entity("WatchWord.Domain.Identity.ApplicationUser", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("AccessFailedCount");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Email")
                    .HasMaxLength(256);

                b.Property<bool>("EmailConfirmed");

                b.Property<bool>("LockoutEnabled");

                b.Property<DateTimeOffset?>("LockoutEnd");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256);

                b.Property<string>("PasswordHash");

                b.Property<string>("PhoneNumber");

                b.Property<bool>("PhoneNumberConfirmed");

                b.Property<string>("SecurityStamp");

                b.Property<bool>("TwoFactorEnabled");

                b.Property<string>("UserName")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
            {
                b.HasOne("WatchWord.Domain.Identity.ApplicationRole")
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
            {
                b.HasOne("WatchWord.Domain.Identity.ApplicationUser")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
            {
                b.HasOne("WatchWord.Domain.Identity.ApplicationUser")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
            {
                b.HasOne("WatchWord.Domain.Identity.ApplicationRole")
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("WatchWord.Domain.Identity.ApplicationUser")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
            {
                b.HasOne("WatchWord.Domain.Identity.ApplicationUser")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("WatchWord.Domain.Entity.Composition", b =>
            {
                b.HasOne("WatchWord.Domain.Entity.Word", "Word")
                    .WithMany()
                    .HasForeignKey("WordId");
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

            modelBuilder.Entity("WatchWord.Domain.Entity.VocabWord", b =>
            {
                b.HasOne("WatchWord.Domain.Entity.Account", "Owner")
                    .WithMany()
                    .HasForeignKey("OwnerId");
            });

            modelBuilder.Entity("WatchWord.Domain.Entity.Word", b =>
            {
                b.HasOne("WatchWord.Domain.Entity.Material", "Material")
                    .WithMany("Words")
                    .HasForeignKey("MaterialId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}

#endif