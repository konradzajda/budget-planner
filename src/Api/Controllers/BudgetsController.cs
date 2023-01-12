using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tivix.BudgetPlanner.Application.Requests.Commands;
using Tivix.BudgetPlanner.Application.Requests.Queries;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/budgets")]
public class BudgetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets single budget by its' unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the budget.</param>
    /// <param name="cancellationToken">A cancellation token used for cancelling asynchronous operations.</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(BudgetViewModel), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:guid}")]
    [ActionName("get-budget-by-id")]
    public async Task<IActionResult> GetBudgetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var request = new GetBudgetByIdQuery
        {
            Id = id,
        };

        var response = await _mediator.Send(request, cancellationToken);

        if (response.Success)
        {
            return Ok(response.Resource);
        }

        return NotFound();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BudgetViewModel>), 200)]
    public async Task<IActionResult> FindBudgetsAsync(
        [FromQuery] string name = null,
        [FromQuery] int offset = 0,
        [FromQuery] int pageSize = 25,
        CancellationToken cancellationToken = default)
    {
        var request = new FindBudgetsQuery
        {
            Name = name,
            Offset = offset,
            PageSize = pageSize,
        };

        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response.Resource);
    }

    [HttpGet("{id:guid}/users")]
    [ActionName("get-budget-users")]
    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBudgetUsersAsync([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetBudgetUsersByIdQuery
        {
            Id = id,
        };

        var response = await _mediator.Send(request, cancellationToken);

        if (response.Success)
        { 
            return Ok(response.Resource);
        }

        return NotFound();
    }

    [ProducesResponseType(typeof(BudgetViewModel),201)]
    [ProducesResponseType(400)]
    [HttpPost]
    public async Task<IActionResult> CreateBudgetAsync([FromBody] CreateBudgetCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.Success)
        {
            // Make sure "Location" header is added to the response
            return CreatedAtAction("get-budget-by-id", new
                {
                    id = response.Resource?.Id,
                },
                response.Resource);
        }

        return StatusCode((int)response.StatusCode, response.Errors);
    }

    [HttpPost("{id:guid}/users")]
    [ProducesResponseType(typeof(IEnumerable<string>),201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddUserAsync([FromRoute] Guid id, [FromBody] AddBudgetUserCommand command,
        CancellationToken cancellationToken = default)
    {
        command.BudgetId = id;
        var response = await _mediator.Send(command, cancellationToken);

        if (response.Success)
        {
            return CreatedAtAction("get-budget-users", new 
            {
                id
            }, response.Resource);
        }

        return StatusCode((int)response.StatusCode, response.Errors);
    }

    [HttpPost("{id:guid}/incomes")]
    [ProducesResponseType(typeof(IEnumerable<BudgetIncomeViewModel>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddIncomeToBudgetAsync(
        [FromRoute] Guid id,
        [FromBody] AddBudgetIncomeCommand command,
        CancellationToken cancellationToken = default)
    {
        command.BudgetId = id;

        var response = await _mediator.Send(command, cancellationToken);

        if (response.Success)
        {
            return Ok(response.Resource);
        }

        return StatusCode((int)response.StatusCode, response.Errors);
    }
}