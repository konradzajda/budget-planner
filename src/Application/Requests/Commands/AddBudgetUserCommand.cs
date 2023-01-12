using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.Requests.Commands;

public class AddBudgetUserCommand : IRequest<IApplicationResponse<IEnumerable<string>>>
{
    [JsonIgnore]
    public Guid BudgetId { get; set; }
    
    public string UserId { get; set; }
}