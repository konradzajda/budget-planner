using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Requests.Commands;

namespace Tivix.BudgetPlanner.Application.Handlers.Commands;

public class RemoveIncomeFromBudgetCommandHandler : IRequestHandler<RemoveIncomeFromBudgetCommand, IApplicationResponse<Unit>>
{
    private readonly IBudgetsContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<RemoveIncomeFromBudgetCommandHandler> _logger;
    private readonly IBudgetGuard _guard;

    public RemoveIncomeFromBudgetCommandHandler(IBudgetsContext context, IMapper mapper, ILogger<RemoveIncomeFromBudgetCommandHandler> logger, IBudgetGuard guard)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _guard = guard;
    }

    public async Task<IApplicationResponse<Unit>> Handle(RemoveIncomeFromBudgetCommand request, CancellationToken cancellationToken)
    {
        if (request.BudgetId == Guid.Empty)
            return ApplicationResponse.NotFound<Unit>();

        var budget = await _context.Budgets
            .Include(y => y.Incomes)
            .SingleOrDefaultAsync(y => y.Id == request.BudgetId, cancellationToken);

        if (budget == null)
            return ApplicationResponse.FromError<Unit>(ErrorMessages.Budget.BudgetNotFound, HttpStatusCode.NotFound);

        var incomeToRemove = budget.Incomes.SingleOrDefault(y => y.Id == request.IncomeId);

        if (incomeToRemove == null)
            return ApplicationResponse.Success(Unit.Value);

        _guard.CanRemoveIncome(incomeToRemove);
        
        budget.Incomes.Remove(incomeToRemove);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred while removing income");
            return ApplicationResponse.FromError<Unit>(ErrorMessages.InternalError, HttpStatusCode.InternalServerError);
        }

        return ApplicationResponse.Success(Unit.Value);
    }
}