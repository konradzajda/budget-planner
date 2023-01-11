using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Queries;
using Tivix.BudgetPlanner.Infrastructure;

namespace Tivix.BudgetPlanner.Application.Handlers.Queries;

public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, IApplicationResponse<BudgetViewModel>>
{
    private readonly IBudgetsContext _context;

    public GetBudgetByIdQueryHandler(IBudgetsContext context)
    {
        _context = context;
    }
    
    public Task<IApplicationResponse<BudgetViewModel>> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = _context.Budgets
            .AsNoTracking()
            .SingleOrDefault(y => y.Id == request.Id);

        if (budget == null)
            return Task.FromResult(ApplicationResponse.NotFound<BudgetViewModel>());

        return Task.FromResult(ApplicationResponse.Success(new BudgetViewModel()));
    }
}