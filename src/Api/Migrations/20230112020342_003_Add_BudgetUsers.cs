using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivix.BudgetPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class _003AddBudgetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetUserEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetUserEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetEntityBudgetUserEntity",
                columns: table => new
                {
                    BudgetsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetEntityBudgetUserEntity", x => new { x.BudgetsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BudgetEntityBudgetUserEntity_BudgetUserEntity_UsersId",
                        column: x => x.UsersId,
                        principalTable: "BudgetUserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetEntityBudgetUserEntity_Budgets_BudgetsId",
                        column: x => x.BudgetsId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetEntityBudgetUserEntity_UsersId",
                table: "BudgetEntityBudgetUserEntity",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetEntityBudgetUserEntity");

            migrationBuilder.DropTable(
                name: "BudgetUserEntity");
        }
    }
}
