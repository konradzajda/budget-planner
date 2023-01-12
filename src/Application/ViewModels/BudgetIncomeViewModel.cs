using System;

namespace Tivix.BudgetPlanner.Application.ViewModels;

public class BudgetIncomeViewModel
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public float Amount { get; set; }
    
    public DateTime CreatedAtUtc { get; set; }
    
    public string CreatedBy { get; set; }
}