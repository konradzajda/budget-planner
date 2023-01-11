using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Entities;
using Tivix.BudgetPlanner.Application.Internal;
using Tivix.BudgetPlanner.Application.Queries;
using Tivix.BudgetPlanner.Infrastructure;
using Xunit;

namespace Tivix.BudgetPlanner.Application.Handlers.Queries;

public class GetBudgetByIdQueryHandlerTests : SqliteBasedTests
{
    private readonly GetBudgetByIdQueryHandler _sut;

    public GetBudgetByIdQueryHandlerTests()
    {
        _sut = new GetBudgetByIdQueryHandler(Context, TestAutoMapper.Instance);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnNotFoundResponse_WhenBudgetNotFound()
    {
        // Given
        var query = new GetBudgetByIdQuery
        {
            Id = Guid.NewGuid(),
        };
        
        // When
        var result = await _sut.Handle(query, CancellationToken.None);
        
        // Then
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedViewModel_WhenBudgetFound()
    {
        // Given
        var query = new GetBudgetByIdQuery
        {
            Id = Guid.NewGuid(),
        };

        var entity = new BudgetEntity
        {
            Id = query.Id,
        };

        await Context.Budgets.AddAsync(entity);
        await Context.SaveChangesAsync();

        
        // When
        var result = await _sut.Handle(query, CancellationToken.None);
        
        // Then
        Assert.NotNull(result.Resource);
        
        
        result.Resource.Id.Should().Be(query.Id);
    }
}