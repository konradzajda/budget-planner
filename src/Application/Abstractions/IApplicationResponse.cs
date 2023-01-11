using System.Net;

namespace Tivix.BudgetPlanner.Application.Abstractions;

public interface IApplicationResponse<out T>
{
    T? Resource { get; }
    
    string[]? Errors { get; }
    
    HttpStatusCode StatusCode { get; }

    bool Success => (Errors == null || Errors.Length == 0);
}