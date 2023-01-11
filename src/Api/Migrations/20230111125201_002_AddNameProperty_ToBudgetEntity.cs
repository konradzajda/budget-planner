using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivix.BudgetPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class _002AddNamePropertyToBudgetEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Budgets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_LastUpdatedAtUtc",
                table: "Budgets",
                column: "LastUpdatedAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Budgets_LastUpdatedAtUtc",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Budgets");
        }
    }
}
