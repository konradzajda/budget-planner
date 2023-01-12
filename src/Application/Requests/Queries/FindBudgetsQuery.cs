using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Api.Controllers;

public class FindBudgetsQuery : IRequest<IApplicationResponse<IEnumerable<BudgetViewModel>>>
{
    public string Name { get; init; }
    
    public int Offset { get; init; }
 
    [JsonPropertyName("page_size")]
    public int PageSize { get; init; }
}