#if MYSQL

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchWord.DataAccess.Migrations
{
    public partial class WordStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Materials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteMaterials_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KnownCount = table.Column<int>(type: "int", nullable: false),
                    LearnCount = table.Column<int>(type: "int", nullable: false),
                    TotalCount = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordStatistics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMaterials_AccountId",
                table: "FavoriteMaterials",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMaterials_MaterialId",
                table: "FavoriteMaterials",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "FavoriteMaterials");

            migrationBuilder.DropTable(
                name: "WordStatistics");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Materials",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

#elif !MYSQL

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchWord.DataAccess.Migrations
{
    public partial class WordStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Materials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteMaterials_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KnownCount = table.Column<int>(type: "int", nullable: false),
                    LearnCount = table.Column<int>(type: "int", nullable: false),
                    TotalCount = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordStatistics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMaterials_AccountId",
                table: "FavoriteMaterials",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMaterials_MaterialId",
                table: "FavoriteMaterials",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "FavoriteMaterials");

            migrationBuilder.DropTable(
                name: "WordStatistics");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Materials",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Accounts_OwnerId",
                table: "Materials",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

#endif