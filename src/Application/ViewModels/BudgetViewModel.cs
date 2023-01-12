using System;
using System.Collections.Generic;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;

namespace Tivix.BudgetPlanner.Application.ViewModels;

public class BudgetViewModel : ITrackable
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime LastUpdatedAtUtc { get; set; }

    public string LastUpdatedBy { get; set; } = string.Empty;

    public IEnumerable<BudgetIncomeViewModel> Incomes { get; set; } = Array.Empty<BudgetIncomeViewModel>();
}