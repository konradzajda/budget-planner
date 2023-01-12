using AspNetCore.Firebase.Authentication.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tivix.BudgetPlanner.Api.Extensions;

namespace Tivix.BudgetPlanner.Api;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        var firebaseProjectId = configuration.GetProjectId();
        
        services.AddFirebaseAuthentication(
            "https://securetoken.google.com/" + firebaseProjectId,
            firebaseProjectId);
        
        services.AddMediatR();
        services.AddControllers();
            
        services.AddEndpointsApiExplorer();
        services.AddSwagger(configuration);
        
        services.AddModelValidation();
    }
}