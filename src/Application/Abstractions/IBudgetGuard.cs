using Tivix.BudgetPlanner.Application.Entities;

namespace Tivix.BudgetPlanner.Application.Abstractions;

/// <summary>
/// Defines methods for validating user's permission for editing budget.
///
/// Normally, it should be in authorization, but setting it up would take too much time for recruitment task.
/// In this scenario, we can use permission based policy like "can-edit-budget:{id}" permission
///
/// </summary>
public interface IBudgetGuard
{
    void CanAddIncome(BudgetEntity budget);
    
    void CanRemoveIncome(BudgetIncomeEntity budget);

    void CanAddUser(BudgetEntity budget);

    void CanRemoveUser(BudgetEntity budget);

    void CanAddExpense(BudgetEntity budget);

    void CanRemoveExpense(BudgetEntity budget);
}