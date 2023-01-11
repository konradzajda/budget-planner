using AutoMapper;

namespace Tivix.BudgetPlanner.Application.Internal;

internal static class TestAutoMapper
{
    internal static IMapper Instance => new Mapper(
        new MapperConfiguration(y =>
        {
            y.AddProfile<BudgetPlannerProfile>();
        }));
}