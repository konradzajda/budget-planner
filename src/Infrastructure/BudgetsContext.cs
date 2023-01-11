using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Infrastructure.Entities;

namespace Tivix.BudgetPlanner.Infrastructure;

public class BudgetsContext : DbContext, IBudgetsContext
{
    
    public DbSet<BudgetEntity> BudgetsSet { get; }

    public IQueryable<BudgetEntity> Budgets => BudgetsSet;
    
    public BudgetsContext(DbContextOptions<BudgetsContext> options) : base(options)
    {
    }
}