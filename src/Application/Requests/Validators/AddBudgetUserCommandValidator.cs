using FluentValidation;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Requests.Commands;

namespace Tivix.BudgetPlanner.Application.Requests.Validators;

public class AddBudgetUserCommandValidator : AbstractValidator<AddBudgetUserCommand>
{
    public AddBudgetUserCommandValidator(IUserContextAccessor contextAccessor)
    {
        RuleFor(y => y.UserId)
            .NotEmpty()
            .NotEqual(contextAccessor.Id)
            .WithMessage("You cannot add yourself to your budget");
    }
}