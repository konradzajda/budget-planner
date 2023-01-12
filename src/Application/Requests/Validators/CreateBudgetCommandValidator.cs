using System.Linq;
using FluentValidation;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Requests.Commands;

namespace Tivix.BudgetPlanner.Application.Requests.Validators;

public sealed class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    private const int NameMinimumLength = 5;

    private const int NameMaximumLength = 50;
    
    public CreateBudgetCommandValidator(IBudgetsContext context, IUserContextAccessor contextAccessor)
    {
        RuleFor(y => y.Name)
            .NotEmpty()
            .MinimumLength(NameMinimumLength)
            .MaximumLength(NameMaximumLength)
            .Must(y => !context.Budgets.Any(b => b.Name == y && b.CreatedBy == contextAccessor.Id))
            .WithMessage("Name must be unique");

    }
}