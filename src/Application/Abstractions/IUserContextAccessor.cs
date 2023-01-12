namespace Tivix.BudgetPlanner.Application.Abstractions;

public interface IUserContextAccessor
{
    public string Email { get; }
    
    public string Id { get; }
}