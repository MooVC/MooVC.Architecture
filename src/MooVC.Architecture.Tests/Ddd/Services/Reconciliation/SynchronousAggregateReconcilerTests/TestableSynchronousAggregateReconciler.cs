namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconcilerTests
{
    using System;
    using MooVC.Architecture.Ddd;

    public sealed class TestableSynchronousAggregateReconciler
        : SynchronousAggregateReconciler
    {
        private readonly Action<EventCentricAggregateRoot[]>? aggregates;
        private readonly Action<DomainEvent[]>? events;

        public TestableSynchronousAggregateReconciler(
            Action<EventCentricAggregateRoot[]>? aggregates = default,
            Action<DomainEvent[]>? events = default)
        {
            this.aggregates = aggregates;
            this.events = events;
        }

        protected override void PerformReconcile(EventCentricAggregateRoot[] aggregates)
        {
            if (this.aggregates is null)
            {
                throw new NotImplementedException();
            }

            this.aggregates(aggregates);
        }

        protected override void PerformReconcile(DomainEvent[] events)
        {
            if (this.events is null)
            {
                throw new NotImplementedException();
            }

            this.events(events);
        }
    }
}