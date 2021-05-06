namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousAggregateReconciler
        : IAggregateReconciler
    {
        public event AggregateConflictDetectedEventHandler? AggregateConflictDetected;

        public event AggregateReconciledEventHandler? AggregateReconciled;

        public event UnsupportedAggregateTypeDetectedEventHandler? UnsupportedAggregateTypeDetected;

        public virtual Task ReconcileAsync(params EventCentricAggregateRoot[] aggregates)
        {
            PerformReconcile(aggregates);

            return Task.CompletedTask;
        }

        public virtual Task ReconcileAsync(params DomainEvent[] events)
        {
            PerformReconcile(events);

            return Task.CompletedTask;
        }

        protected abstract void PerformReconcile(EventCentricAggregateRoot[] aggregates);

        protected abstract void PerformReconcile(DomainEvent[] events);

        protected virtual void OnAggregateConflictDetected(
            Reference aggregate,
            IEnumerable<DomainEvent> events,
            SignedVersion next,
            SignedVersion previous)
        {
            AggregateConflictDetected?.Invoke(
                this,
                new AggregateConflictDetectedEventArgs(
                    aggregate,
                    events,
                    next,
                    previous));
        }

        protected virtual void OnAggregateReconciled(
            Reference aggregate,
            IEnumerable<DomainEvent> events)
        {
            AggregateReconciled?.Invoke(
                this,
                new AggregateReconciledEventArgs(aggregate, events));
        }

        protected virtual void OnUnsupportedAggregateTypeDetected(Type type)
        {
            UnsupportedAggregateTypeDetected?.Invoke(
                this,
                new UnsupportedAggregateTypeDetectedEventArgs(type));
        }
    }
}