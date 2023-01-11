using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Requests.Commands;

public class CreateBudgetCommand : IRequest<IApplicationResponse<BudgetViewModel>>
{
    public string Name { get; init; } = string.Empty;
}