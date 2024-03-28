using Sinqia.App.Enum;
using Sinqia.App.Factory;
using Sinqia.App.Factory.Interfaces;
using Sinqia.App.Models;
using Sinqia.App.Observable;
using Sinqia.App.Services.Interfaces;

namespace Sinqia.App.Services
{
    public class UserValidator : IUserValidator
    {
        IUserFactory _userFactory;
        private const string UserCreationErrorMsg = "Error creating the user";
        private const string UserCreationSuccessMsg = "Create the user: {0} for the type: {1}";

        public UserValidator(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public Task<bool> ValidatePassword(string password)
        {
            return Task.FromResult(!string.IsNullOrEmpty(password) && password.Length >= 6);
        }

        public Task<bool> ValidateUsername(string username)
        {
            return Task.FromResult(!string.IsNullOrEmpty(username));
        }

        public async Task<bool> ValidateUser(User user)
        {
            bool isPasswordValid = await ValidatePassword(user.Password);
            bool isUsernameValid = await ValidateUsername(user.Username);

            return isPasswordValid && isUsernameValid;
        }

        public async Task<string> CreateNewUser(UserType userType, User user)
        {
            var response = await _userFactory.CreateUser(userType, user.Username, user.Password);
            if (response == null)
                return UserCreationErrorMsg;
            else
                return string.Format(UserCreationSuccessMsg, response.Username, userType);
        }

        public async Task<string> TestObservable()
        {
            ObservableUser user = new() { Username = "OldUsername" };            
            UserObserver observer = new(user);            
            return user.Username = "NewUsername";            
        }

    }
}
