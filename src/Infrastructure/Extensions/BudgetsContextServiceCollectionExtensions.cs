using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tivix.BudgetPlanner.Infrastructure;

namespace Tivix.BudgetPlanner.Api.Extensions;

public static class BudgetsContextServiceCollectionExtensions
{
    private const string BudgetContextConnectionStringKey = "MsSql";
    
    public static void AddBudgetsContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(BudgetContextConnectionStringKey);

        services.AddDbContext<BudgetsContext>(y =>
        {
            y.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("tivix_api"));
        });
    }
}