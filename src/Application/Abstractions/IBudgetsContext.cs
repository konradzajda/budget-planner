using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tivix.BudgetPlanner.Infrastructure.Entities;

namespace Tivix.BudgetPlanner.Application.Abstractions;

/// <summary>
/// Defines methods and properties for budgets context.
/// </summary>
public interface IBudgetsContext
{
    /// <summary>
    /// Gets budgets.
    /// </summary>
    IQueryable<BudgetEntity> Budgets { get; }

    /// <summary>
    /// Saves changes applied to the context.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token used for cancelling asynchronous operations.</param>
    /// <returns>Count of entities updated.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}