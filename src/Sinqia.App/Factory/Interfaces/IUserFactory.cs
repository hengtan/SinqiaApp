using Sinqia.App.Enum;
using Sinqia.App.Models;

namespace Sinqia.App.Factory.Interfaces
{
    public interface IUserFactory
    {
       Task<User> CreateUser(UserType userType, string username, string password);
    }
}
