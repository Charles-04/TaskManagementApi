using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Persistence.Migrations
{
    public partial class Configurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UserProfiles_AssigneeId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UserProfiles_AssigneeId1",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UserProfiles_AuthorId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UserProfiles_AuthorId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AuthorId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AssigneeId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UserProfiles_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UserProfiles_AuthorId",
                table: "Tasks",
                column: "AuthorId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UserProfiles_AssigneeId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UserProfiles_AuthorId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeId1",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId1",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId1",
                table: "Tasks",
                column: "AssigneeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AuthorId1",
                table: "Tasks",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UserProfiles_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UserProfiles_AssigneeId1",
                table: "Tasks",
                column: "AssigneeId1",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UserProfiles_AuthorId",
                table: "Tasks",
                column: "AuthorId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UserProfiles_AuthorId1",
                table: "Tasks",
                column: "AuthorId1",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
