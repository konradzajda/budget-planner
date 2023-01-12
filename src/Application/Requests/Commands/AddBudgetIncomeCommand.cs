using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application.Requests.Commands;

public class AddBudgetIncomeCommand : IRequest<IApplicationResponse<IEnumerable<BudgetIncomeViewModel>>>
{
    [JsonIgnore]
    public Guid BudgetId { get; set; }
    
    public string Title { get; set; }
    
    public float Amount { get; set; }
}