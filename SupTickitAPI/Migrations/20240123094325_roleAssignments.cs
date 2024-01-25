using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupTickitAPI.Migrations
{
    /// <inheritdoc />
    public partial class roleAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleAssignment_Users_UserId",
                table: "RoleAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleAssignment",
                table: "RoleAssignment");

            migrationBuilder.RenameTable(
                name: "RoleAssignment",
                newName: "RoleAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_RoleAssignment_UserId",
                table: "RoleAssignments",
                newName: "IX_RoleAssignments_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleAssignments",
                table: "RoleAssignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAssignments_Users_UserId",
                table: "RoleAssignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleAssignments_Users_UserId",
                table: "RoleAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleAssignments",
                table: "RoleAssignments");

            migrationBuilder.RenameTable(
                name: "RoleAssignments",
                newName: "RoleAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_RoleAssignments_UserId",
                table: "RoleAssignment",
                newName: "IX_RoleAssignment_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleAssignment",
                table: "RoleAssignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAssignment_Users_UserId",
                table: "RoleAssignment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
