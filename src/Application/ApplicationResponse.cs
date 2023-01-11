using System;
using System.Linq;
using System.Net;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application;

/// <summary>
/// Provides static methods to construct <see cref="IApplicationResponse{T}"/>. 
/// </summary>
internal static class ApplicationResponse
{
    /// <summary>
    /// Initializes new instance of <see cref="IApplicationResponse{T}"/> default implementation,
    /// describing successful application response with resource.
    /// </summary>
    /// <param name="resource">See <see cref="IApplicationResponse{T}.Resource"/></param>
    /// <param name="statusCode">See <see cref="IApplicationResponse{T}.StatusCode"/></param>
    /// <typeparam name="T">Type of the resource, see <see cref="IApplicationResponse{T}.Resource"/></typeparam>
    /// <returns>A successful <see cref="IApplicationResponse{T}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when resource is null.</exception>
    /// <exception cref="ArgumentException">Thrown when status code is not successful status code (4xx, 5xx)</exception>
    internal static IApplicationResponse<T> Success<T>(T resource, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource));

        if ((short)statusCode >= 400)
            throw new ArgumentException(
                "HTTP Status Code must be successful when initiating resource-full application response",
                nameof(statusCode));
        
        return new DefaultApplicationResponse<T>(resource, statusCode);
    }

    /// <summary>
    /// Initializes new instance of <see cref="IApplicationResponse{T}"/> implementation,
    /// describing unsuccessful application response with <see cref="HttpStatusCode.NotFound"/> HTTP status code.
    /// </summary>
    /// <typeparam name="T">Type of the resource, see <see cref="IApplicationResponse{T}.Resource"/></typeparam>
    /// <returns>An unsuccessful <see cref="IApplicationResponse{T}"/>.</returns>
    internal static IApplicationResponse<T> NotFound<T>()
    {
        return new DefaultApplicationResponse<T>(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Initializes new instance of <see cref="IApplicationResponse{T}"/> default implementation,
    /// describing unsuccessful application response with single error message.
    /// </summary>
    /// <param name="errorMessage">An error message.</param>
    /// <param name="statusCode">See <see cref="IApplicationResponse{T}.StatusCode"/></param>
    /// <typeparam name="T">Type of the resource, see <see cref="IApplicationResponse{T}.Resource"/></typeparam>
    /// <returns>An unsuccessful <see cref="IApplicationResponse{T}"/>.</returns>
    internal static IApplicationResponse<T> FromError<T>(
        string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return FromError<T>(new[] { errorMessage }, statusCode);
    }

    /// <summary>
    /// Initializes new instance of <see cref="IApplicationResponse{T}"/> default implementation,
    /// describing unsuccessful application response with at least one error message.
    /// </summary>
    /// <param name="errorMessages">Error messages.</param>
    /// <param name="statusCode">See <see cref="IApplicationResponse{T}.StatusCode"/></param>
    /// <typeparam name="T">Type of the resource, see <see cref="IApplicationResponse{T}.Resource"/></typeparam>
    /// <returns>An unsuccessful <see cref="IApplicationResponse{T}"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="errorMessages"/>is empty or contains empty strings only.</exception>
    internal static IApplicationResponse<T> FromError<T>(
        string[] errorMessages,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        errorMessages = errorMessages.Select(y => y.Trim())
            .Where(y => !string.IsNullOrEmpty(y))
            .ToArray();
            
        if (errorMessages.Length == 0)
            throw new ArgumentException(
                "Unsuccessful application response must contain at least one error message",
                nameof(errorMessages));

        if ((ushort) statusCode < 400)
            throw new ArgumentException(
                "HTTP Status code must be unsuccessful (4xx, 5xx) when initiating error application response",
                nameof(statusCode));

        return new DefaultApplicationResponse<T>(errorMessages, statusCode);
    }

    private struct DefaultApplicationResponse<T> : IApplicationResponse<T>
    {
        public T? Resource { get; }
    
        public string[]? Errors { get; }
    
        public HttpStatusCode StatusCode { get; }
    
        internal DefaultApplicationResponse(T resource, HttpStatusCode statusCode)
        {
            Resource = resource;
            StatusCode = statusCode;
        }

        internal DefaultApplicationResponse(HttpStatusCode statusCode)
        {
            Errors = null;
            StatusCode = statusCode;
        }

        internal DefaultApplicationResponse(string[] errors, HttpStatusCode statusCode)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
    }
}

