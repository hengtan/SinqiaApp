using Sinqia.App.Configuration.Interface;

namespace Sinqia.App.Configuration
{
    public class UserManager : IUserManager
    {
        private static UserManager? _instance;
        private static readonly object _lock = new();

        private UserManager()
        {
        }

        public static UserManager Instance
        {
            get
            {
                lock (_lock)
                {
                    _instance ??= new UserManager();
                    return _instance;
                }
            }
        }
    }
}
