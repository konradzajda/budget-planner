using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Application.Queries;
using Xunit;

namespace Tivix.BudgetPlanner.Api.Controllers;

public class BudgetsControllerTests
{
    private readonly BudgetsController _sut;

    private readonly IMediator _mediator;

    public BudgetsControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new BudgetsController(_mediator);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnOk_WhenBudgetFound()
    {
        // Given
        var viewModel = new BudgetViewModel();
        var applicationResponse = Substitute.For<IApplicationResponse<BudgetViewModel>>();
        var id = Guid.NewGuid();
        
        applicationResponse.Resource.Returns(viewModel);
        applicationResponse.Success.Returns(true);
        

        _mediator.Send(Arg.Is<GetBudgetByIdQuery>(y => y.Id == id), Arg.Any<CancellationToken>())
            .Returns(applicationResponse);
        
        // When
        var result = await _sut.GetByIdAsync(id);
        
        // Then
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnBudget_InResponseContent()
    {
        // Given
        var viewModel = new BudgetViewModel();
        var applicationResponse = Substitute.For<IApplicationResponse<BudgetViewModel>>();
        var id = Guid.NewGuid();
        
        applicationResponse.Resource.Returns(viewModel);
        applicationResponse.Success.Returns(true);


        _mediator.Send(Arg.Is<GetBudgetByIdQuery>(y => y.Id == id))
            .Returns(applicationResponse);
        
        // When
        var result = await _sut.GetByIdAsync(id) as OkObjectResult;
        
        // Then
        
        // Double assertion to avoid possible null reference warning
        Assert.NotNull(result);
        result.Value.Should().BeOfType<BudgetViewModel>()
            .And
            .Be(viewModel);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnNotFound_WhenResourceIsNull()
    {
        // Given
        var id = Guid.NewGuid();
        var applicationResponse = Substitute.For<IApplicationResponse<BudgetViewModel>>();

        applicationResponse.Resource.ReturnsNull();
        applicationResponse.Success.Returns(false);

        _mediator.Send(Arg.Is<GetBudgetByIdQuery>(y => y.Id == id))
            .Returns(applicationResponse);
        
        // When
        var result = await _sut.GetByIdAsync(id);
        
        // Then
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetByIdAsync_Should_PassCancellationTokenParameter_ToMediator()
    {
        // Given
        var token = new CancellationToken();
        
        // When
        var _ = await _sut.GetByIdAsync(Guid.NewGuid(), token);
        
        // Then
        await _mediator.Received().Send(Arg.Any<GetBudgetByIdQuery>(), token);

    }
}