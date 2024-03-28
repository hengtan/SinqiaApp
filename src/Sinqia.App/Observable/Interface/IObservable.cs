namespace Sinqia.App.Observable.Interface
{
    public interface IObservable
    {
        event Action StateChanged;
    }
}
