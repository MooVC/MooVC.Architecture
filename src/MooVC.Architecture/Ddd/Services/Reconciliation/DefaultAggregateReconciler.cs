namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd;
    using MooVC.Collections.Generic;
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

        public override void Reconcile(params EventCentricAggregateRoot[] aggregates)
        {
            if (aggregates.Any())
            {
                foreach (IGrouping<Type, EventCentricAggregateRoot> aggregateTypes in aggregates.GroupBy(aggregate => aggregate.GetType()))
                {
                    IAggregateReconciliationProxy? proxy = factory(aggregateTypes.Key);

                    if (proxy is null)
                    {
                        OnUnsupportedAggregateTypeDetected(aggregateTypes.Key);
                    }
                    else
                    {
                        aggregateTypes.ForEach(aggregate => Reconcile(aggregate, proxy));
                    }
                }
            }
        }

        public override void Reconcile(params DomainEvent[] events)
        {
            if (events.Any())
            {
                foreach (IGrouping<Type, DomainEvent> aggregateTypes in events.GroupBy(@event => @event.Aggregate.Type))
                {
                    IAggregateReconciliationProxy? proxy = factory(aggregateTypes.Key);

                    if (proxy is null)
                    {
                        OnUnsupportedAggregateTypeDetected(aggregateTypes.Key);
                    }
                    else
                    {
                        foreach (IGrouping<VersionedReference, DomainEvent> aggregateEvents in aggregateTypes.GroupBy(@event => @event.Aggregate))
                        {
                            if (EventsAreNonConflicting(aggregateEvents.Key, aggregateEvents, out _))
                            {
                                Reconcile(aggregateEvents.Key, aggregateEvents, proxy);
                            }
                        }
                    }
                }
            }
        }

        private void PerformCoordinatedReconcile(EventCentricAggregateRoot aggregate, IAggregateReconciliationProxy proxy)
        {
            proxy.Overwrite(aggregate);
        }

        private void PerformCoordinatedReconcile(
           Reference aggregate,
           IEnumerable<DomainEvent> events,
           IAggregateReconciliationProxy proxy)
        {
            EventCentricAggregateRoot existing = proxy.Get(aggregate);

            if (existing is null)
            {
                existing = proxy.Create(aggregate);
            }
            else if (ignorePreviousVersions)
            {
                events = RemovePreviousVersions(events, existing.Version);
            }

            Apply(existing, events, proxy, aggregate);
        }

        private void Reconcile(
            Reference aggregate,
            IEnumerable<DomainEvent> events,
            IAggregateReconciliationProxy proxy)
        {
            aggregate.Coordinate(
                () => PerformCoordinatedReconcile(aggregate, events, proxy),
                timeout: timeout);
        }

        private void Reconcile(EventCentricAggregateRoot aggregate, IAggregateReconciliationProxy proxy)
        {
            aggregate.Coordinate(
                () => PerformCoordinatedReconcile(aggregate, proxy),
                timeout: timeout);
        }
    }
}