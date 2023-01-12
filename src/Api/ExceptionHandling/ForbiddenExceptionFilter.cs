using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tivix.BudgetPlanner.Application.Exceptions;

namespace Tivix.BudgetPlanner.Api.ExceptionHandling;

public class ForbiddenExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is InsufficientPermissionsException)
        {
            context.Result = new ForbidResult();
        }
    }
}