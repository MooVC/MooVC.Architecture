namespace MooVC.Architecture.Ddd.Services
{
    using System.Threading.Tasks;

    public abstract class SynchronousBus
        : Bus
    {
        protected override async Task PerformPublishAsync(params DomainEvent[] events)
        {
            PerformPublish(events);

            await Task.CompletedTask;
        }

        protected abstract void PerformPublish(params DomainEvent[] events);
    }
}