namespace MooVC.Architecture.Ddd.Services
{
    public interface IStopSaga<T>
        where T : DomainEvent
    {
        void Stop(T @event);
    }
}