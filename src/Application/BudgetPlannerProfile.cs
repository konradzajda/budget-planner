using AutoMapper;
using Tivix.BudgetPlanner.Application.Entities;
using Tivix.BudgetPlanner.Application.Queries;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Application;

public class BudgetPlannerProfile : Profile
{
    public BudgetPlannerProfile()
    {
        CreateProjection<BudgetEntity, BudgetViewModel>();
    }
}