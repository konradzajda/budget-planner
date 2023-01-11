using Microsoft.Extensions.DependencyInjection;

namespace Tivix.BudgetPlanner.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(y =>
        {
            y.AddProfile<BudgetPlannerProfile>();
        });
    }
}