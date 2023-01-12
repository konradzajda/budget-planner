using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;
using Tivix.BudgetPlanner.Application.Requests.Commands;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Handlers.Commands;

public class AddBudgetIncomeCommandHandler : IRequestHandler<AddBudgetIncomeCommand, IApplicationResponse<IEnumerable<BudgetIncomeViewModel>>>
{
    private readonly IBudgetsContext _context;
    private readonly IUserContextAccessor _contextAccessor;
    private readonly ILogger<AddBudgetIncomeCommandHandler> _logger;
    private readonly IBudgetGuard _guard;
    private readonly IMapper _mapper;

    public AddBudgetIncomeCommandHandler(IBudgetsContext context, IUserContextAccessor contextAccessor, ILogger<AddBudgetIncomeCommandHandler> logger, IBudgetGuard guard, IMapper mapper)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _logger = logger;
        _guard = guard;
        _mapper = mapper;
    }

    public async Task<IApplicationResponse<IEnumerable<BudgetIncomeViewModel>>> Handle(AddBudgetIncomeCommand request,
        CancellationToken cancellationToken)
    {
        if (request.BudgetId == Guid.Empty)
            return ApplicationResponse.NotFound<IEnumerable<BudgetIncomeViewModel>>();

        var budget = await _context
            .Budgets
            .Include(y => y.Incomes)
            .SingleOrDefaultAsync(y => y.Id == request.BudgetId, cancellationToken);

        if (budget == null)
            return ApplicationResponse.NotFound<IEnumerable<BudgetIncomeViewModel>>();

        _guard.CanAddIncome(budget);
        
        var income = new BudgetIncomeEntity
        {
            Title = request.Title,
            Amount = request.Amount,
            CreatedBy = _contextAccessor.Id,
            CreatedAtUtc = DateTime.UtcNow,
            Budget = budget,
        };
        
        budget.Incomes.Add(income);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred while adding income to the budget");
            return ApplicationResponse.FromError<IEnumerable<BudgetIncomeViewModel>>(
                ErrorMessages.InternalError,
                HttpStatusCode.InternalServerError);
        }

        var viewModels = budget.Incomes.Select(_mapper.Map<BudgetIncomeViewModel>);
        
        return ApplicationResponse.Success(viewModels);
    }
}