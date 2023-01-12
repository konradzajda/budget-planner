using System;
using System.Collections.Generic;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.Requests.Queries;

public class GetBudgetUsersByIdQuery : IRequest<IApplicationResponse<IEnumerable<string>>>
{
    public Guid Id { get; init; }
}