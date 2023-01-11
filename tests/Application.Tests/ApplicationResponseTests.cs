using System;
using System.Net;
using FluentAssertions;
using Xunit;

namespace Tivix.BudgetPlanner.Application;

public class ApplicationResponseTests
{
    [Fact]
    public void Success_ShouldReturnApplicationResponse_WithValidResource()
    {
        // Given
        var resource = new object();
        
        // When
        var response = ApplicationResponse.Success(resource);
        
        // Then
        response.Resource.Should().Be(resource);
    }

    [Fact]
    public void Success_ShouldReturnApplicationResponse_WithEmptyErrorMessagesArray()
    {
        // When
        var response = ApplicationResponse.Success(new object());
        
        // Then
        response.Errors.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Success_ShouldReturnApplicationResponse_WithSuccessValueEqTrue()
    {
        // When
        var response = ApplicationResponse.Success(new object());
        
        // Then
        response.Success.Should().BeTrue();
    }

    [Theory]
    [InlineData(HttpStatusCode.Accepted)]
    [InlineData(HttpStatusCode.NotModified)]
    public void Success_ShouldReturnApplicationResponse_WithValidHttpStatusCode(HttpStatusCode statusCode)
    {
        // When
        var response = ApplicationResponse.Success(new object(), statusCode);
        
        // Then
        response.StatusCode.Should().Be(statusCode);
    }

    [Fact]
    public void Success_ShouldThrowArgumentNullException_WhenResourceIsNull()
    {
        // When
        Action call = () => ApplicationResponse.Success<object>(null!);
        
        // Then
        call.Should().ThrowExactly<ArgumentNullException>()
            .Which
            .ParamName
            .Should().Be("resource");
    }

    [Theory]
    [InlineData(HttpStatusCode.NotImplemented)]
    [InlineData(HttpStatusCode.BadRequest)]
    public void Success_ShouldThrowArgumentException_WhenStatusCodeNotSuccessful(HttpStatusCode statusCode)
    {
        // When
        Action call = () => ApplicationResponse.Success(new object(), statusCode);
        
        // Then
        call.Should().ThrowExactly<ArgumentException>()
            .Which
            .ParamName
            .Should().Be("statusCode");
    }
}