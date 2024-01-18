using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Migrations
{
    public partial class FirstMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ftesat_Nets_NetsNetId",
                table: "Ftesat");

            migrationBuilder.DropIndex(
                name: "IX_Ftesat_NetsNetId",
                table: "Ftesat");

            migrationBuilder.DropColumn(
                name: "NetsNetId",
                table: "Ftesat");

            migrationBuilder.RenameColumn(
                name: "WeddingId",
                table: "Ftesat",
                newName: "NetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ftesat_NetId",
                table: "Ftesat",
                column: "NetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ftesat_Nets_NetId",
                table: "Ftesat",
                column: "NetId",
                principalTable: "Nets",
                principalColumn: "NetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ftesat_Nets_NetId",
                table: "Ftesat");

            migrationBuilder.DropIndex(
                name: "IX_Ftesat_NetId",
                table: "Ftesat");

            migrationBuilder.RenameColumn(
                name: "NetId",
                table: "Ftesat",
                newName: "WeddingId");

            migrationBuilder.AddColumn<int>(
                name: "NetsNetId",
                table: "Ftesat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ftesat_NetsNetId",
                table: "Ftesat",
                column: "NetsNetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ftesat_Nets_NetsNetId",
                table: "Ftesat",
                column: "NetsNetId",
                principalTable: "Nets",
                principalColumn: "NetId");
        }
    }
}
