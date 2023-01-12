using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivix.BudgetPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class _004AddBudgetIncome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetIncomeEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "serial", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetIncomeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetIncomeEntity_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetIncomeEntity_BudgetId",
                table: "BudgetIncomeEntity",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetIncomeEntity_CreatedAtUtc",
                table: "BudgetIncomeEntity",
                column: "CreatedAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetIncomeEntity");
        }
    }
}
