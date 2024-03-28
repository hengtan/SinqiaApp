using Sinqia.App.Observable.Interface;

namespace Sinqia.App.Observable
{
    public class ObservableUser : IObservable
    {
        public event Action StateChanged;

        private string? _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                StateChanged?.Invoke();
            }
        }
    }
}
