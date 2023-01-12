using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;

namespace Tivix.BudgetPlanner.Infrastructure;

public class BudgetsContext : DbContext, IBudgetsContext
{
    private readonly IUserContextAccessor _contextAccessor;
    public DbSet<BudgetEntity> Budgets { get; set; }
    
    public BudgetsContext(DbContextOptions<BudgetsContext> options, IUserContextAccessor contextAccessor) : base(options)
    {
        _contextAccessor = contextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BudgetEntity>()
            .HasKey(y => y.Id);

        modelBuilder.Entity<BudgetEntity>()
            .HasIndex(y => y.LastUpdatedAtUtc);

        modelBuilder.Entity<BudgetEntity>()
            .HasMany(y => y.Users)
            .WithMany(y => y.Budgets);

        modelBuilder.Entity<BudgetUserEntity>()
            .HasKey(y => y.Id);

        modelBuilder.Entity<BudgetIncomeEntity>()
            .HasKey(y => y.Id);

        modelBuilder.Entity<BudgetIncomeEntity>()
            .HasIndex(y => y.CreatedAtUtc);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var addedEntities = ChangeTracker.Entries<ITrackable>()
            .Where(e => e.State == EntityState.Added)
            .ToArray();

        var modifiedEntities = ChangeTracker.Entries<ITrackable>()
            .Where(y => y.State == EntityState.Modified)
            .Concat(addedEntities);

        foreach (var entity in addedEntities)
        {
            entity.Property<DateTime>(nameof(ITrackable.CreatedAtUtc)).CurrentValue = DateTime.UtcNow;
            entity.Property<string>(nameof(ITrackable.CreatedBy)).CurrentValue = _contextAccessor.Id;
        }

        foreach (var entity in modifiedEntities)
        {
            entity.Property<DateTime>(nameof(ITrackable.LastUpdatedAtUtc)).CurrentValue = DateTime.UtcNow;
            entity.Property<string>(nameof(ITrackable.LastUpdatedBy)).CurrentValue = _contextAccessor.Id;
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}