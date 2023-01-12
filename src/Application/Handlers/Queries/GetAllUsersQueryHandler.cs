using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Requests.Queries;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Handlers.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IApplicationResponse<IEnumerable<UserViewModel>>>
{
    private readonly IUsersFinder _finder;

    public GetAllUsersQueryHandler(IUsersFinder finder)
    {
        _finder = finder;
    }
    
    public async Task<IApplicationResponse<IEnumerable<UserViewModel>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _finder.GetAllUsersAsync(cancellationToken);
        
        return ApplicationResponse.Success(users);
    }
}