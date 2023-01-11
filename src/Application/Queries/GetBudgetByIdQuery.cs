using System;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.Queries;

public class GetBudgetByIdQuery : IRequest<IApplicationResponse<BudgetViewModel>>
{
    public Guid Id { get; init; }
}

public class BudgetViewModel
{
}