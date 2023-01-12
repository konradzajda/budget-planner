using FluentValidation;
using Tivix.BudgetPlanner.Application.Requests.Commands;

namespace Tivix.BudgetPlanner.Application.Requests.Validators;

public class AddBudgetIncomeCommandValidator : AbstractValidator<AddBudgetIncomeCommand>
{
    private const ushort TitleMinimumLength = 5;

    private const ushort TitleMaximumLength = 32;
    
    public AddBudgetIncomeCommandValidator()
    {
        RuleFor(y => y.Title)
            .MinimumLength(TitleMinimumLength)
            .MaximumLength(TitleMaximumLength);

        RuleFor(y => y.Amount)
            .GreaterThan(0);
    }
}