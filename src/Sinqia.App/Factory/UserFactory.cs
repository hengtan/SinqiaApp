using Sinqia.App.Enum;
using Sinqia.App.Factory.Interfaces;
using Sinqia.App.Models;

namespace Sinqia.App.Factory
{
    public class UserFactory : IUserFactory
    {
        public async Task<User> CreateUser(UserType type, string username, string password)
        {
            return type switch
            {
                UserType.Admin => new AdminUser { Username = username, Password = password, AdminRights = "All" },
                UserType.Regular => new RegularUser { Username = username, Password = password, Permissions = "Limited" },
                _ => throw new ArgumentException("Invalid user type"),
            };
        }
    }
}
