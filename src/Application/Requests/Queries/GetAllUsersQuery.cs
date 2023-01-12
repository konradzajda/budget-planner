using System.Collections.Generic;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Requests.Queries;

public class GetAllUsersQuery : IRequest<IApplicationResponse<IEnumerable<UserViewModel>>>
{
    
}