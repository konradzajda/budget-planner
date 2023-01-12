using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;
using Tivix.BudgetPlanner.Application.Requests.Commands;

namespace Tivix.BudgetPlanner.Application.Handlers.Commands;

public class AddBudgetUserCommandHandler : IRequestHandler<AddBudgetUserCommand, IApplicationResponse<IEnumerable<string>>>
{
    private readonly IBudgetsContext _context;
    private readonly IUserContextAccessor _contextAccessor;
    private readonly IUsersFinder _finder;
    private readonly ILogger<AddBudgetUserCommandHandler> _logger;
    private readonly IBudgetGuard _guard;

    public AddBudgetUserCommandHandler(
        IBudgetsContext context,
        IUserContextAccessor contextAccessor,
        IUsersFinder finder,
        ILogger<AddBudgetUserCommandHandler> logger, IBudgetGuard guard)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _finder = finder;
        _logger = logger;
        _guard = guard;
    }
    
    public async Task<IApplicationResponse<IEnumerable<string>>> Handle(AddBudgetUserCommand request, CancellationToken cancellationToken)
    {
        if (request.BudgetId == Guid.Empty)
            return ApplicationResponse.NotFound<IEnumerable<string>>();
        
        var budget = await _context.Budgets
            .SingleOrDefaultAsync(y => y.Id == request.BudgetId, cancellationToken);

        if (budget == null)
        {
            return ApplicationResponse.FromError<IEnumerable<string>>(
                ErrorMessages.Budget.BudgetNotFound,
                HttpStatusCode.NotFound);
        }
        
        _guard.CanAddUser(budget);

        if (budget.Users.Any(y => y.Id == request.UserId))
        {
            return ApplicationResponse.FromError<IEnumerable<string>>(
                ErrorMessages.Budget.UserAlreadyAdded);
        }

        var userExists = await _finder.UserExistsAsync(request.UserId, cancellationToken);

        if (!userExists)
        {
            return ApplicationResponse.FromError<IEnumerable<string>>(
                ErrorMessages.Users.UserNotFound,
                HttpStatusCode.NotFound);
        }

        var user = new BudgetUserEntity
        {
            Id = request.UserId,
        };
        
        budget.Users.Add(user);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred while adding user to the budget");
            return ApplicationResponse.FromError<IEnumerable<string>>(
                ErrorMessages.InternalError,
                HttpStatusCode.InternalServerError);
        }

        var users = budget.Users.Select(y => y.Id);
        
        return ApplicationResponse.Success(users);
    }
}