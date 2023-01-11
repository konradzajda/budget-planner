using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tivix.BudgetPlanner.Api.Extensions;

public static class ModelValidationServiceCollectionExtensions
{
    public static void AddModelValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(Application.DependencyInjection).Assembly);
    }
}