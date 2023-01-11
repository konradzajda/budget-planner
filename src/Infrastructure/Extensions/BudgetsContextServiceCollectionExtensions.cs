using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Infrastructure.Extensions;

public static class BudgetsContextServiceCollectionExtensions
{
    private const string BudgetContextConnectionStringKey = "Postgres";
    
    public static void AddBudgetsContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(BudgetContextConnectionStringKey);

        services.AddDbContext<IBudgetsContext, BudgetsContext>(y =>
        {
            y.UseNpgsql(connectionString, postgres =>
            {
                postgres.MigrationsAssembly("tivix_api");
            });
        });
    }
}