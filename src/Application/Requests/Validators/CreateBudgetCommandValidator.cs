using FluentValidation;
using Tivix.BudgetPlanner.Application.Requests.Commands;

namespace Tivix.BudgetPlanner.Application.Requests.Validators;

public sealed class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    private const int NameMinimumLength = 5;

    private const int NameMaximumLength = 50;
    
    public CreateBudgetCommandValidator()
    {
        RuleFor(y => y.Name)
            .NotEmpty()
            .MinimumLength(NameMinimumLength)
            .MaximumLength(NameMaximumLength);
    }
}