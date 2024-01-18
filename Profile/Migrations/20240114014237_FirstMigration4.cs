using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Migrations
{
    public partial class FirstMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ftesat",
                columns: table => new
                {
                    FtesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    WeddingId = table.Column<int>(type: "int", nullable: true),
                    NetsNetId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ftesat", x => x.FtesaId);
                    table.ForeignKey(
                        name: "FK_Ftesat_Nets_NetsNetId",
                        column: x => x.NetsNetId,
                        principalTable: "Nets",
                        principalColumn: "NetId");
                    table.ForeignKey(
                        name: "FK_Ftesat_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Ftesat_NetsNetId",
                table: "Ftesat",
                column: "NetsNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ftesat_UserId",
                table: "Ftesat",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ftesat");
        }
    }
}
