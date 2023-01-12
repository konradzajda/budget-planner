namespace Tivix.BudgetPlanner.Application;

public static class ErrorMessages
{
    public const string InternalError = "INTERNAL_ERROR";

    public static class Budget
    {
        public const string BudgetNotFound = "BUDGET_NOT_FOUND";
        public const string UserAlreadyAdded = "USER_ALREADY_ADDED";
    }

    public static class Users
    {
        public const string UserNotFound = "USER_NOT_FOUND";
    }
}