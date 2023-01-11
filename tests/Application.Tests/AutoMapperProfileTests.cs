using System;
using AutoMapper;
using Xunit;

namespace Tivix.BudgetPlanner.Application;

public class AutoMapperProfileTests
{
    [Theory]
    [InlineData(typeof(BudgetPlannerProfile))]
    public void AutoMapperConfigurations_Should_BeValid(Type profileType)
    {
        var config = new MapperConfiguration(
            cfg=> cfg.AddProfile(profileType));
        config.AssertConfigurationIsValid();
    }
}