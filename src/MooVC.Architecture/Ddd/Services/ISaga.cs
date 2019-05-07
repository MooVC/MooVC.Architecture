namespace MooVC.Architecture.Ddd.Services
{
    public interface ISaga<TStart, TStop>
        where TStart : DomainEvent
        where TStop : DomainEvent
    {
        void Start(TStart @event);

        void Stop(TStop @event);
    }
}