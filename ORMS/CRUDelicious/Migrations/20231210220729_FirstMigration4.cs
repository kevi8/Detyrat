using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDelicious.Migrations
{
    public partial class FirstMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Chefes_ChefId",
                table: "Dishes");

            migrationBuilder.DropTable(
                name: "Chefes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_ChefId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Dishes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChefId",
                table: "Dishes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chefes",
                columns: table => new
                {
                    ChefId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefes", x => x.ChefId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_ChefId",
                table: "Dishes",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Chefes_ChefId",
                table: "Dishes",
                column: "ChefId",
                principalTable: "Chefes",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
