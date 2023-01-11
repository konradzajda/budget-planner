using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tivix.BudgetPlanner.Application.Queries;

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

    [HttpGet("{id:guid}")]
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
}