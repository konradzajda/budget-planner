using System.Collections.Generic;

namespace Tivix.BudgetPlanner.Application.Entities;

public class BudgetUserEntity
{
    public string Id { get; set; }
    
    public virtual List<BudgetEntity> Budgets { get; set; }
}