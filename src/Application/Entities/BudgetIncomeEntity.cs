using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tivix.BudgetPlanner.Application.Entities;

public class BudgetIncomeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public float Amount { get; set; }
    
    public DateTime CreatedAtUtc { get; set; }
    
    public string CreatedBy { get; set; }
    
    public virtual BudgetEntity Budget { get; set; }
}