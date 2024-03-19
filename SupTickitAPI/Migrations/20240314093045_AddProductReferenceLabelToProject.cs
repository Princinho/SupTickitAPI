using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupTickitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProductReferenceLabelToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductReferenceName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductReferenceName",
                table: "Projects");
        }
    }
}
