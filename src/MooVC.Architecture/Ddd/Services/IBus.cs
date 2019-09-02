namespace MooVC.Architecture.Ddd.Services
{
    public interface IBus
    {
        void Publish(params DomainEvent[] events);
    }
}