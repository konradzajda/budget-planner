using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Infrastructure.Entities;

namespace Tivix.BudgetPlanner.Infrastructure;

public class BudgetsContext : DbContext
{
    public DbSet<BudgetEntity> Budgets { get; set; }

    public BudgetsContext(DbContextOptions<BudgetsContext> options) : base(options)
    {
    }
}