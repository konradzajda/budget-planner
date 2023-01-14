using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

public class FindBudgetsQueryHandler : IRequestHandler<FindBudgetsQuery, IApplicationResponse<IEnumerable<BudgetViewModel>>>
{
    private readonly IBudgetsContext _context;
    private readonly IMapper _mapper;
    private readonly IUserContextAccessor _contextAccessor;

    public FindBudgetsQueryHandler(
        IBudgetsContext context, IMapper mapper, IUserContextAccessor contextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }
    
    public async Task<IApplicationResponse<IEnumerable<BudgetViewModel>>> Handle(FindBudgetsQuery request, CancellationToken cancellationToken)
    {
        var offset = request.Offset < 0 ? 0 : request.Offset;
        var pageSize = request.PageSize <= 0 ? 25 : request.PageSize;

        var budgets = await _context.Budgets
            .Where(y => string.IsNullOrEmpty(request.Name) ||
                        y.Name.Equals(request.Name))
            .Where(y => y.Users.Any(u => u.Id == _contextAccessor.Id) || y.CreatedBy.Equals(_contextAccessor.Id))
            .OrderByDescending(y => y.LastUpdatedAtUtc)
            .Skip(offset)
            .Take(pageSize)
            .ProjectTo<BudgetViewModel>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return ApplicationResponse.Success(budgets);
    }
}