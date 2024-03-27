using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupTickitAPI.Migrations
{
    /// <inheritdoc />
    public partial class tobApplied : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuoteTaxOrBonus");

            migrationBuilder.CreateTable(
                name: "TaxOrBonusApplied",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxOrBonusId = table.Column<int>(type: "int", nullable: false),
                    QuoteId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxOrBonusApplied", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxOrBonusApplied_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxOrBonusApplied_TaxOrBonuses_TaxOrBonusId",
                        column: x => x.TaxOrBonusId,
                        principalTable: "TaxOrBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxOrBonusApplied_QuoteId",
                table: "TaxOrBonusApplied",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxOrBonusApplied_TaxOrBonusId",
                table: "TaxOrBonusApplied",
                column: "TaxOrBonusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxOrBonusApplied");

            migrationBuilder.CreateTable(
                name: "QuoteTaxOrBonus",
                columns: table => new
                {
                    QuotesId = table.Column<int>(type: "int", nullable: false),
                    TaxOrBonusesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteTaxOrBonus", x => new { x.QuotesId, x.TaxOrBonusesId });
                    table.ForeignKey(
                        name: "FK_QuoteTaxOrBonus_Quotes_QuotesId",
                        column: x => x.QuotesId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteTaxOrBonus_TaxOrBonuses_TaxOrBonusesId",
                        column: x => x.TaxOrBonusesId,
                        principalTable: "TaxOrBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteTaxOrBonus_TaxOrBonusesId",
                table: "QuoteTaxOrBonus",
                column: "TaxOrBonusesId");
        }
    }
}
