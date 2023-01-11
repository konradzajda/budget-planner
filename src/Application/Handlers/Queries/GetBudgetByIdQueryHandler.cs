using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Queries;

namespace Tivix.BudgetPlanner.Application.Handlers.Queries;

public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, IApplicationResponse<BudgetViewModel>>
{
    public Task<IApplicationResponse<BudgetViewModel>> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ApplicationResponse.NotFound<BudgetViewModel>());
    }
}