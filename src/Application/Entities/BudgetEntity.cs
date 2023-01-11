using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.Entities;

[Table("Budgets")]
public class BudgetEntity : ITrackable
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAtUtc { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime LastUpdatedAtUtc { get; set; }

    public string LastUpdatedBy { get; set; } = string.Empty;
}