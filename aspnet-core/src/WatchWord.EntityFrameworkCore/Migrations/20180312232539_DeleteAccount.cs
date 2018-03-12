using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WatchWord.Migrations
{
    public partial class DeleteAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMaterials_Accounts_AccountId",
                table: "FavoriteMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_VocabWords_Accounts_OwnerId",
                table: "VocabWords");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "VocabWords",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "AccountId",
                table: "FavoriteMaterials",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMaterials_AbpUsers_AccountId",
                table: "FavoriteMaterials",
                column: "AccountId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_AbpUsers_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VocabWords_AbpUsers_OwnerId",
                table: "VocabWords",
                column: "OwnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMaterials_AbpUsers_AccountId",
                table: "FavoriteMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_AbpUsers_OwnerId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_VocabWords_AbpUsers_OwnerId",
                table: "VocabWords");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "VocabWords",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccountId",
                table: "FavoriteMaterials",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Boolean = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Int = table.Column<int>(nullable: false),
                    Key = table.Column<int>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false),
                    String = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Accounts_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ExternalId",
                table: "Accounts",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Key",
                table: "Settings",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_OwnerId",
                table: "Settings",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMaterials_Accounts_AccountId",
                table: "FavoriteMaterials",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VocabWords_Accounts_OwnerId",
                table: "VocabWords",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
