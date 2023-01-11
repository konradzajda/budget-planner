using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Application.Entities;

[Table("Budgets")]
public class BudgetEntity : ITrackable
{

    // As it's somehow finance application, unique identifier of budget is not numeric
    // To secure API from exposing ids by iteration
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public DateTime CreatedAtUtc { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime LastUpdatedAtUtc { get; set; }

    public string LastUpdatedBy { get; set; } = string.Empty;
}