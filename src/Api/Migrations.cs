using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tivix.BudgetPlanner.Infrastructure;

namespace Tivix.BudgetPlanner.Api;

/// <summary>
/// Simple class to ensure database is created and migrated without developer typing any weird commands.
/// </summary>
internal static class WebApplicationMigrationsExtensions
{
    internal static async Task RunDatabaseMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<BudgetsContext>();
        
        if (!context.Database.IsNpgsql())
            throw new InvalidOperationException("Only SQL server is supported for now. Using No SQL would be too easy.");

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        
        if (pendingMigrations.Any())
            await context.Database.MigrateAsync();
    }
}