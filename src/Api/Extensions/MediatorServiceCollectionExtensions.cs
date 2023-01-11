using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Tivix.BudgetPlanner.Api.Extensions;

public static class MediatorServiceCollectionExtensions
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(
            typeof(Api.DependencyInjection),
            typeof(Application.DependencyInjection),
            typeof(Infrastructure.DependencyInjection));
    }
}