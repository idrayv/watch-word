using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WatchWord.Migrations
{
    public partial class VocabWordStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_AbpUsers_OwnerId",
                table: "Materials");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Materials",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VocabWordStatistics",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CorrectGuesses = table.Column<int>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false),
                    Word = table.Column<string>(nullable: false),
                    WrongGuesses = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocabWordStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VocabWordStatistics_AbpUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocabWordStatistics_OwnerId",
                table: "VocabWordStatistics",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_AbpUsers_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_AbpUsers_OwnerId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "VocabWordStatistics");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Materials",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_AbpUsers_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
