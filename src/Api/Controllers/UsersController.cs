using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tivix.BudgetPlanner.Application.Requests.Queries;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // In production scenario, this should be filtered by some groups, or should have some kind of search
    // To avoid exposing all users to the any user.
    // It has been exposed this way to make your life easier while testing POST budgets/{id}/users
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), 200)]
    public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
    {
        var request = new GetAllUsersQuery();

        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response.Resource);
    }
}