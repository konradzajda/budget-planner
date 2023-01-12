using System.Linq;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;
using Tivix.BudgetPlanner.Application.Exceptions;

namespace Tivix.BudgetPlanner.Application;

public class BudgetGuard : IBudgetGuard
{
    private readonly IUserContextAccessor _contextAccessor;

    private string CurrentUserId => _contextAccessor.Id;

    public BudgetGuard(IUserContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    
    public void CanAddIncome(BudgetEntity budget)
    {
        ExceptionHelper(nameof(CanAddIncome), IsAddedToBudget(budget));
    }

    public void CanRemoveIncome(BudgetIncomeEntity income)
    {
        ExceptionHelper(nameof(CanRemoveIncome), IsCreator(income) || IsCreator(income.Budget));
    }

    public void CanAddUser(BudgetEntity budget)
    {
        ExceptionHelper(nameof(CanAddUser), IsCreator(budget));
    }

    public void CanRemoveUser(BudgetEntity budget)
    {
        ExceptionHelper(nameof(CanRemoveUser), IsCreator(budget));
    }

    public void CanAddExpense(BudgetEntity budget)
    {
        ExceptionHelper(nameof(CanAddExpense), IsAddedToBudget(budget));
    }

    public void CanRemoveExpense(BudgetEntity budget)
    {
        ExceptionHelper(nameof(CanRemoveExpense), IsCreator(budget));
    }

    private static void ExceptionHelper(string permissionName, bool hasPermission)
    {
        if (!hasPermission)
            throw new InsufficientPermissionsException(permissionName);
    }
    
    private bool IsCreator(ITrackable budget) => budget.CreatedBy.Equals(CurrentUserId);

    private bool IsCreator(BudgetIncomeEntity income) => income.CreatedBy.Equals(CurrentUserId);

    private bool IsAddedToBudget(BudgetEntity budget) =>
        IsCreator(budget) || budget.Users.Any(y => y.Id == CurrentUserId);

}