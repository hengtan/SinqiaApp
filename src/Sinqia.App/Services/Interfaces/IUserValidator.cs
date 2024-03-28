using Sinqia.App.Enum;
using Sinqia.App.Models;

namespace Sinqia.App.Services.Interfaces
{
    public interface IUserValidator
    {
        Task<bool> ValidateUser(User user);
        Task<bool> ValidatePassword(string password);
        Task<bool> ValidateUsername(string username);
        Task<string> CreateNewUser(UserType userType, User user);
        Task<string> TestObservable();
    }
}
