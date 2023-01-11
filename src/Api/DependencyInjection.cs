using Microsoft.Extensions.DependencyInjection;
using Tivix.BudgetPlanner.Api.Extensions;

namespace Tivix.BudgetPlanner.Api;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddControllers();
            
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddModelValidation();
    }
}