using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;

namespace Tivix.BudgetPlanner.Infrastructure;

public class BudgetsContext : DbContext, IBudgetsContext
{
    public DbSet<BudgetEntity> Budgets { get; set; }
    
    public BudgetsContext(DbContextOptions<BudgetsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BudgetEntity>()
            .HasKey(y => y.Id);

        modelBuilder.Entity<BudgetEntity>()
            .HasIndex(y => y.LastUpdatedAtUtc);
    }
}