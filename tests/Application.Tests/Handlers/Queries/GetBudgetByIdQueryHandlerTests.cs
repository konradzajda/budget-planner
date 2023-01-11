using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Queries;
using Tivix.BudgetPlanner.Infrastructure.Entities;
using Xunit;

namespace Tivix.BudgetPlanner.Application.Handlers.Queries;

public class GetBudgetByIdQueryHandlerTests
{
    private readonly GetBudgetByIdQueryHandler _sut;
    private readonly IBudgetsContext _context;

    public GetBudgetByIdQueryHandlerTests()
    {
        _context = Substitute.For<IBudgetsContext>();
        _sut = new GetBudgetByIdQueryHandler(_context);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnNotFoundResponse_WhenBudgetNotFound()
    {
        // Given
        var query = new GetBudgetByIdQuery
        {
            Id = Guid.NewGuid(),
        };
        
        _context.Budgets.Returns(Array.Empty<BudgetEntity>().AsQueryable());
        
        // When
        var result = await _sut.Handle(query, CancellationToken.None);
        
        // Then
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}