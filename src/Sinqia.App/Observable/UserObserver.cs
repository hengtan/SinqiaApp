using Sinqia.App.Observable.Interface;

namespace Sinqia.App.Observable
{
    public class UserObserver : IObserver
    {
        private readonly ObservableUser _user;

        public UserObserver(ObservableUser user)
        {
            _user = user;
            _user.StateChanged += Update;
        }

        public void Update()
        {
            Console.WriteLine($"User's state has changed. New username: {_user.Username}");
        }

    }
}
