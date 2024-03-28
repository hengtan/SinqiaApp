using Xunit;
using Sinqia.App.Services;
using Sinqia.App.Models;
using Sinqia.App.Factory.Interfaces;
using Sinqia.App.Factory;

namespace Sinqia.Tests.Entities
{
    public class UserValidatorTests
    {
        private readonly UserValidator _validator;
        private readonly IUserFactory userFactory;

        public UserValidatorTests()
        {
            userFactory = new UserFactory();
            _validator = new UserValidator(userFactory);
        }

        [Xunit.Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("username", true)]
        public void ValidateUsername_ReturnsExpectedResult(string username, bool expectedResult)
        {
            // Act
            bool result = _validator.ValidateUsername(username).Result;

            // Assert
            Xunit.Assert.Equal(expectedResult, result);
        }

        [Xunit.Theory]
        [InlineData("12345", false)]
        [InlineData("12345678", true)]
        public void ValidatePassword_ReturnsExpectedResult(string password, bool expectedResult)
        {
            // Act
            bool result = _validator.ValidatePassword(password).Result;

            // Assert
            Xunit.Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ValidateUser_ReturnsExpectedResult()
        {
            // Arrange
            var user = new User { Username = "username", Password = "12345678" };

            // Act
            bool result = _validator.ValidateUser(user).Result;

            // Assert
            Xunit.Assert.True(result);
        }
    }
}