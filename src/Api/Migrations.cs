using System;
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
        await using var context = app.Services.GetRequiredService<BudgetsContext>();

        if (!context.Database.IsSqlServer())
            throw new InvalidOperationException("Only SQL server is supported for now. Using No SQL would be too easy.");

        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
    }
}