using System;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.ViewModels;

public class BudgetViewModel : ITrackable
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime LastUpdatedAtUtc { get; set; }

    public string LastUpdatedBy { get; set; } = string.Empty;
}