using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planer.Migrations
{
    public partial class FirstMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Plans",
                newName: "TimeNow");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Plans",
                newName: "End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeNow",
                table: "Plans",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Plans",
                newName: "CreatedAt");
        }
    }
}
