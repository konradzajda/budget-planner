using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Handlers.Commands;
using Tivix.BudgetPlanner.Application.Internal;
using Tivix.BudgetPlanner.Application.Requests.Commands;
using Xunit;

namespace Tivix.BudgetPlanner.Application.Handlers.Requests;

public class CreateBudgetCommandHandlerTests : SqliteBasedTests
{
    private readonly CreateBudgetCommandHandler _sut;

    public CreateBudgetCommandHandlerTests()
    {
        _sut = new CreateBudgetCommandHandler(Context, TestAutoMapper.Instance,
            NullLogger<CreateBudgetCommandHandler>.Instance);
    }

    [Fact]
    public async Task Handle_Should_ReturnInternalServerError_OnException()
    {
        // Given
        var context = Substitute.For<IBudgetsContext>();
        context.Budgets.Throws(new Exception());

        var sut = new CreateBudgetCommandHandler(context, null!, NullLogger<CreateBudgetCommandHandler>.Instance);
        
        // When
        var result = await sut.Handle(new CreateBudgetCommand(), CancellationToken.None);
        
        // Then
        result.Errors.Should().Contain(ErrorMessages.InternalError);
        result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Handle_Should_AddBudgetToContext_WithValidName()
    {
        // Given
        const string budgetName = nameof(budgetName);

        var command = new CreateBudgetCommand
        {
            Name = budgetName,
        };
        
        // When
        var result = await _sut.Handle(command, CancellationToken.None);
        
        // Then
        Context.Budgets.Should().ContainSingle(y => string.Equals(budgetName, y.Name));
    }
}