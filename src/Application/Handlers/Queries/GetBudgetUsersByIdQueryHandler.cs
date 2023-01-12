using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Requests.Queries;

namespace Tivix.BudgetPlanner.Application.Handlers.Queries;

public class GetBudgetUsersByIdQueryHandler : IRequestHandler<GetBudgetUsersByIdQuery, IApplicationResponse<IEnumerable<string>>>
{
    private readonly IBudgetsContext _context;
    private readonly IUserContextAccessor _contextAccessor;

    public GetBudgetUsersByIdQueryHandler(IBudgetsContext context, IUserContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }
    
    public async Task<IApplicationResponse<IEnumerable<string>>> Handle(GetBudgetUsersByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = await _context.Budgets
            .Include(y => y.Users)
            .SingleOrDefaultAsync(y => y.Id == request.Id, cancellationToken);

        if (budget == null)
        {
            return ApplicationResponse.NotFound<IEnumerable<string>>();
        }

        // Normally, I would use some policy provider and put it closer to authorization.
        if (!string.Equals(_contextAccessor.Email, budget.CreatedBy))
        {
            // Returning NotFound instead of Unauthorized, because we potentially don't want to
            // Say other clients that there actually is some budget with provided id.
            return ApplicationResponse.NotFound<IEnumerable<string>>();
        }

        var users = budget.Users.Select(y => y.Id);

        return ApplicationResponse.Success(users);
    }
}