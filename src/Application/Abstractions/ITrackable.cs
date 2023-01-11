using System;

namespace Tivix.BudgetPlanner.Application.Abstractions;

public interface ITrackable
{
    public DateTime CreatedAtUtc { get; set; }
    
    public string CreatedBy { get; set; }
    
    public DateTime LastUpdatedAtUtc { get; set; }
    
    public string LastUpdatedBy { get; set; }
}