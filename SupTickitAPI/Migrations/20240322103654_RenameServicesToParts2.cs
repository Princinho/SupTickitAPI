using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupTickitAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameServicesToParts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_PartsCategories_ServiceCategoryId",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "ServiceCategoryId",
                table: "Parts",
                newName: "PartCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_ServiceCategoryId",
                table: "Parts",
                newName: "IX_Parts_PartCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_PartsCategories_PartCategoryId",
                table: "Parts",
                column: "PartCategoryId",
                principalTable: "PartsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_PartsCategories_PartCategoryId",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "PartCategoryId",
                table: "Parts",
                newName: "ServiceCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_PartCategoryId",
                table: "Parts",
                newName: "IX_Parts_ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_PartsCategories_ServiceCategoryId",
                table: "Parts",
                column: "ServiceCategoryId",
                principalTable: "PartsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
