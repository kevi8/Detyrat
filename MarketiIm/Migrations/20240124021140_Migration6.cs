using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketiIm.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SPassword",
                table: "Stores",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Stores",
                newName: "SPassword");
        }
    }
}
