using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Auth;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Handlers.Commands;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Infrastructure;

public class FirebaseUsersFinder : IUsersFinder
{
    private readonly IMapper _mapper;

    public FirebaseUsersFinder(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userId, cancellationToken);

        return user != null;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var allUsers = new List<ExportedUserRecord>();
        
        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator(cancellationToken);
        while (await responses.MoveNextAsync())
        {
            var response = responses.Current;
            allUsers.AddRange(response.Users);
        }

        return allUsers
            .Select(_mapper.Map<UserViewModel>);
    }
}