using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planer.Migrations
{
    public partial class FirstMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Plans",
                newName: "PlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "Plans",
                newName: "PostId");
        }
    }
}
