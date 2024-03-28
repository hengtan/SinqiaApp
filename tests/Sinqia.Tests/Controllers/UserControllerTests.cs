using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Sinqia.App.Controllers;
using Sinqia.App.Services.Interfaces;
using Sinqia.App.Models;
using Assert = Xunit.Assert;
using Sinqia.App.Enum;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace Sinqia.Tests.Controllers;
public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly Mock<IUserValidator> _mockValidator;

    public UserControllerTests()
    {
        _mockValidator = new Mock<IUserValidator>();
        _controller = new UserController(_mockValidator.Object);
    }

    [Fact]
    public async Task ValidateUser_ReturnsOkResult_WhenUserIsValid()
    {
        // Arrange
        var user = new User { Username = "username", Password = "12345678" };
        _mockValidator.Setup(v => v.ValidateUser(user)).ReturnsAsync(true);

        // Act
        var result = await _controller.ValidateUser(user);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ValidateUser_ReturnsBadRequestResult_WhenUserIsInvalid()
    {
        // Arrange
        var user = new User { Username = "", Password = "12345" };
        _mockValidator.Setup(v => v.ValidateUser(user)).ReturnsAsync(false);

        // Act
        var result = await _controller.ValidateUser(user);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ValidateUser_ReturnsBadRequestResult_WhenPasswordIsInvalid()
    {
        // Arrange
        var user = new User { Username = "username", Password = "12345" };
        _mockValidator.Setup(v => v.ValidateUser(user)).ReturnsAsync(false);

        // Act
        var result = await _controller.ValidateUser(user);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [InlineData(UserType.Admin)]
    [InlineData(UserType.Regular)]
    public async Task CreateNewUser_ReturnsOkResult_WhenUserIsValid(UserType userType)
    {
        // Arrange
        var user = new User { Username = "username", Password = "12345678" };
        _mockValidator.Setup(v => v.CreateNewUser(userType, user)).ReturnsAsync("User created");

        // Act
        var result = await _controller.CreateNewUser(userType, user);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("User created", okResult.Value);
    }

    [Theory]
    [InlineData(UserType.Admin)]
    [InlineData(UserType.Regular)]
    public async Task CreateNewUser_ReturnsBadRequestResult_WhenUserIsInvalid(UserType userType)
    {
        // Arrange
        var user = new User { Username = "", Password = "12345" };
        _mockValidator.Setup(v => v.CreateNewUser(UserType.Regular, user)).ReturnsAsync((string)null);

        // Act
        var result = await _controller.CreateNewUser(userType, user);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateObservableUser_ReturnsOkResult()
    {
        // Arrange
        _mockValidator.Setup(v => v.TestObservable()).ReturnsAsync("Observable user created");

        // Act
        var result = await _controller.CreateObservableUser();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Observable user created", okResult.Value);
    }
}
