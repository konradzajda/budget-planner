using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tivix.BudgetPlanner.Application.Requests.Commands;
using Tivix.BudgetPlanner.Application.Requests.Queries;

namespace Tivix.BudgetPlanner.Api.Controllers;

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
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:guid}")]
    [ActionName("get-budget-by-id")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
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

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
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
}