using System;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.Requests.Commands;

public class RemoveIncomeFromBudgetCommand : IRequest<IApplicationResponse<Unit>>
{
    public Guid BudgetId { get; init; }
    
    public int IncomeId { get; init; }
}