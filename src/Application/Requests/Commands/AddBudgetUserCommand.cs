using System;
using System.Collections.Generic;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.Requests.Commands;

public class AddBudgetUserCommand : IRequest<IApplicationResponse<IEnumerable<string>>>
{
    public string UserId { get; set; }
    public Guid BudgetId { get; set; }
}