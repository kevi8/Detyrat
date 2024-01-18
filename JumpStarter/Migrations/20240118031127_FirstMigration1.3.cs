using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JumpStarter.Migrations
{
    public partial class FirstMigration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAndProject_Projects_ProjectId",
                table: "UserAndProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAndProject_Users_UserId",
                table: "UserAndProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAndProject",
                table: "UserAndProject");

            migrationBuilder.RenameTable(
                name: "UserAndProject",
                newName: "UsersAndProjects");

            migrationBuilder.RenameIndex(
                name: "IX_UserAndProject_UserId",
                table: "UsersAndProjects",
                newName: "IX_UsersAndProjects_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAndProject_ProjectId",
                table: "UsersAndProjects",
                newName: "IX_UsersAndProjects_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAndProjects",
                table: "UsersAndProjects",
                column: "UserAndProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAndProjects_Projects_ProjectId",
                table: "UsersAndProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAndProjects_Users_UserId",
                table: "UsersAndProjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersAndProjects_Projects_ProjectId",
                table: "UsersAndProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersAndProjects_Users_UserId",
                table: "UsersAndProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAndProjects",
                table: "UsersAndProjects");

            migrationBuilder.RenameTable(
                name: "UsersAndProjects",
                newName: "UserAndProject");

            migrationBuilder.RenameIndex(
                name: "IX_UsersAndProjects_UserId",
                table: "UserAndProject",
                newName: "IX_UserAndProject_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersAndProjects_ProjectId",
                table: "UserAndProject",
                newName: "IX_UserAndProject_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAndProject",
                table: "UserAndProject",
                column: "UserAndProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndProject_Projects_ProjectId",
                table: "UserAndProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndProject_Users_UserId",
                table: "UserAndProject",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
