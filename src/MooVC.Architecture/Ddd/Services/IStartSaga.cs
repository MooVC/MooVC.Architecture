namespace MooVC.Architecture.Ddd.Services
{
    public interface IStartSaga<T>
        where T : DomainEvent
    {
        void Start(T @event);
    }
}