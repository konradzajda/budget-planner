using System;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Queries;

public class GetBudgetByIdQuery : IRequest<IApplicationResponse<BudgetViewModel>>
{
    public Guid Id { get; init; }
}