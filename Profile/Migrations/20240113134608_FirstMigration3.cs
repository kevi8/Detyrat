using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Migrations
{
    public partial class FirstMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Net_Users_UserId",
                table: "Net");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Net",
                table: "Net");

            migrationBuilder.RenameTable(
                name: "Net",
                newName: "Nets");

            migrationBuilder.RenameIndex(
                name: "IX_Net_UserId",
                table: "Nets",
                newName: "IX_Nets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nets",
                table: "Nets",
                column: "NetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nets_Users_UserId",
                table: "Nets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nets_Users_UserId",
                table: "Nets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nets",
                table: "Nets");

            migrationBuilder.RenameTable(
                name: "Nets",
                newName: "Net");

            migrationBuilder.RenameIndex(
                name: "IX_Nets_UserId",
                table: "Net",
                newName: "IX_Net_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Net",
                table: "Net",
                column: "NetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Net_Users_UserId",
                table: "Net",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
