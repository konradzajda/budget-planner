using Microsoft.Extensions.DependencyInjection;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IBudgetGuard, BudgetGuard>();
    }
}