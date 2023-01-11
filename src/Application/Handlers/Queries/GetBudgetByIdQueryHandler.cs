using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Requests.Queries;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Handlers.Queries;

public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, IApplicationResponse<BudgetViewModel>>
{
    private readonly IBudgetsContext _context;
    private readonly IMapper _mapper;

    public GetBudgetByIdQueryHandler(IBudgetsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IApplicationResponse<BudgetViewModel>> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            return ApplicationResponse.NotFound<BudgetViewModel>();
        
        var budget = await _context.Budgets
            .ProjectTo<BudgetViewModel>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .SingleOrDefaultAsync(y => y.Id == request.Id, cancellationToken);

        if (budget == null)
            return ApplicationResponse.NotFound<BudgetViewModel>();

        return ApplicationResponse.Success(budget);
    }
}