using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketiIm.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KPassword",
                table: "Klients",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Klients",
                newName: "KPassword");
        }
    }
}
