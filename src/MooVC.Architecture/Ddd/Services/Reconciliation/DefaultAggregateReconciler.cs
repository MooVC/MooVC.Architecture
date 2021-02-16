namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    public sealed class DefaultAggregateReconciler
        : AggregateReconciler
    {
        private readonly Func<Type, IAggregateReconciliationProxy?> factory;
        private readonly bool ignorePreviousVersions;
        private readonly TimeSpan? timeout;

        public DefaultAggregateReconciler(
            Func<Type, IAggregateReconciliationProxy?> factory,
            bool ignorePreviousVersions = true,
            TimeSpan? timeout = default)
        {
            ArgumentNotNull(factory, nameof(factory), DefaultAggregateReconcilerFactoryRequired);

            this.factory = factory;
            this.ignorePreviousVersions = ignorePreviousVersions;
            this.timeout = timeout;
        }

        public override async Task ReconcileAsync(params EventCentricAggregateRoot[] aggregates)
        {
            if (aggregates.Any())
            {
                foreach (IGrouping<Type, EventCentricAggregateRoot> aggregateTypes in aggregates
                    .GroupBy(aggregate => aggregate.GetType()))
                {
                    IAggregateReconciliationProxy? proxy = factory(aggregateTypes.Key);

                    if (proxy is null)
                    {
                        OnUnsupportedAggregateTypeDetected(aggregateTypes.Key);
                    }
                    else
                    {
                        foreach (EventCentricAggregateRoot aggregate in aggregateTypes)
                        {
                            await ReconcileAsync(aggregate, proxy)
                                .ConfigureAwait(false);
                        }
                    }
                }
            }
        }

        public override async Task ReconcileAsync(params DomainEvent[] events)
        {
            if (events.Any())
            {
                foreach (IGrouping<Type, DomainEvent> aggregateTypes in events
                    .GroupBy(@event => @event.Aggregate.Type))
                {
                    IAggregateReconciliationProxy? proxy = factory(aggregateTypes.Key);

                    if (proxy is null)
                    {
                        OnUnsupportedAggregateTypeDetected(aggregateTypes.Key);
                    }
                    else
                    {
                        foreach (IGrouping<VersionedReference, DomainEvent> aggregateEvents in aggregateTypes
                            .GroupBy(@event => @event.Aggregate))
                        {
                            if (EventsAreNonConflicting(aggregateEvents.Key, aggregateEvents, out _))
                            {
                                await ReconcileAsync(aggregateEvents.Key, aggregateEvents, proxy)
                                    .ConfigureAwait(false);
                            }
                        }
                    }
                }
            }
        }

        private Task PerformCoordinatedReconcileAsync(
            EventCentricAggregateRoot aggregate,
            IAggregateReconciliationProxy proxy)
        {
            return proxy.OverwriteAsync(aggregate);
        }

        private async Task PerformCoordinatedReconcileAsync(
           Reference aggregate,
           IEnumerable<DomainEvent> events,
           IAggregateReconciliationProxy proxy)
        {
            EventCentricAggregateRoot? existing = await proxy
                .GetAsync(aggregate)
                .ConfigureAwait(false);

            if (existing is null)
            {
                existing = await proxy
                    .CreateAsync(aggregate)
                    .ConfigureAwait(false);
            }
            else if (ignorePreviousVersions)
            {
                events = RemovePreviousVersions(events, existing.Version);
            }

            await ApplyAsync(existing, events, proxy, aggregate)
                .ConfigureAwait(false);
        }

        private Task ReconcileAsync(
            Reference aggregate,
            IEnumerable<DomainEvent> events,
            IAggregateReconciliationProxy proxy)
        {
            return aggregate.CoordinateAsync(
                () => PerformCoordinatedReconcileAsync(aggregate, events, proxy),
                timeout: timeout);
        }

        private Task ReconcileAsync(
            EventCentricAggregateRoot aggregate,
            IAggregateReconciliationProxy proxy)
        {
            return aggregate.CoordinateAsync(
                () => PerformCoordinatedReconcileAsync(aggregate, proxy),
                timeout: timeout);
        }
    }
}