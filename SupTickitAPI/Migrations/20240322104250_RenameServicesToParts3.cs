using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupTickitAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameServicesToParts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "QuoteDetail",
                newName: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteDetail_PartId",
                table: "QuoteDetail",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteDetail_Parts_PartId",
                table: "QuoteDetail",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteDetail_Parts_PartId",
                table: "QuoteDetail");

            migrationBuilder.DropIndex(
                name: "IX_QuoteDetail_PartId",
                table: "QuoteDetail");

            migrationBuilder.RenameColumn(
                name: "PartId",
                table: "QuoteDetail",
                newName: "ServiceId");
        }
    }
}
