namespace MooVC.Architecture.Ddd.Services
{
    public interface IBus
    {
        void Publish<T>(T @event)
            where T : DomainEvent;
    }
}