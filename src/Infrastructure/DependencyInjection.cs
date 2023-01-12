using System.IO;
using System.Linq;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Infrastructure.Extensions;

namespace Tivix.BudgetPlanner.Infrastructure;

public static class DependencyInjection
{
    private const string CredentialFileName = "this_should_not_be_in_git.json";
    
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // Add as "Scoped" to user info values at private field throughout http request.
        services.AddScoped<IUserContextAccessor, UserContextAccessor>();
        services.AddTransient<IUsersFinder, FirebaseUsersFinder>();
        
        // Notice: Budgets Context must be added AFTER IUserContextAccessor
        services.AddBudgetsContext(configuration);

        var filePath = Directory.GetFiles(Directory.GetCurrentDirectory())
            .Single(y => y.EndsWith(CredentialFileName));

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(filePath),
        });
    }
}