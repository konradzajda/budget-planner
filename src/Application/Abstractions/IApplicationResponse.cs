using System.Net;

namespace Tivix.BudgetPlanner.Application.Abstractions;

/// <summary>
/// Defines properties for standardized application response model.
/// Application response describes result of the client's (for example, API's client) request.
/// </summary>
/// <typeparam name="T">Type of resource requested.</typeparam>
public interface IApplicationResponse<out T>
{
    /// <summary>
    /// Gets resource.
    ///
    /// Resource is an object served by API, for example single budget, multiple expenses, query result etc.
    /// </summary>
    T? Resource { get; }
    
    /// <summary>
    /// Gets error messages describing issue with completing client's request.
    /// </summary>
    string[]? Errors { get; }
    
    /// <summary>
    /// Gets HTTP status code that should be returned to API's client as a result.
    /// </summary>
    HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets a value indicating whether application response is successful.
    /// </summary>
    bool Success => (Errors == null || Errors.Length == 0);
}