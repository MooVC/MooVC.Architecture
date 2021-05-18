namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class SynchronousBus
        : Bus
    {
        protected override Task PerformPublishAsync(
            IEnumerable<DomainEvent> events,
            CancellationToken? cancellationToken = default)
        {
            PerformPublish(events);

            return Task.CompletedTask;
        }

        protected abstract void PerformPublish(IEnumerable<DomainEvent> events);
    }
}