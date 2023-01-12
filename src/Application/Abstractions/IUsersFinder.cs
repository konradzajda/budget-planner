using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Abstractions;

public interface IUsersFinder
{
    Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync(CancellationToken cancellationToken);
}