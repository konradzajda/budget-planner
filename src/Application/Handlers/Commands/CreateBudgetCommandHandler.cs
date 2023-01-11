using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;
using Tivix.BudgetPlanner.Application.Requests.Commands;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Handlers.Commands;

public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, IApplicationResponse<BudgetViewModel>>
{
    private readonly IBudgetsContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBudgetCommandHandler> _logger;

    public CreateBudgetCommandHandler(IBudgetsContext context, IMapper mapper, ILogger<CreateBudgetCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<IApplicationResponse<BudgetViewModel>> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetEntity = new BudgetEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        EntityEntry<BudgetEntity> entry;

        try
        {
            entry = await _context.Budgets.AddAsync(budgetEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        // TODO: Catch DbUpdateException and return key violations too.
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred while adding new budget to the database");
            return ApplicationResponse.FromError<BudgetViewModel>(ErrorMessages.InternalError,
                HttpStatusCode.InternalServerError);
        }

        var viewModel = _mapper.Map<BudgetViewModel>(entry.Entity);
        
        return ApplicationResponse.Success<BudgetViewModel>(viewModel);
    }
}