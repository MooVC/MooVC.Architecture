namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd;

    public abstract class AggregateReconciler
        : IAggregateReconciler
    {
        public event AggregateConflictDetectedEventHandler AggregateConflictDetected;

        public event AggregateReconciledEventHandler AggregateReconciled;

        public event UnsupportedAggregateDetectedEventHandler UnsupportedAggregateDetected;

        public abstract void Reconcile(IEnumerable<DomainEvent> events);

        protected virtual bool EventsAreNonConflicting(VersionedReference aggregate, IEnumerable<DomainEvent> events, out bool isNew)
        {
            IEnumerable<SignedVersion> versions = events.Select(@event => @event.Aggregate.Version).Distinct();
            SignedVersion previous = versions.First();

            isNew = previous.IsNew;

            foreach (SignedVersion next in versions.Skip(1))
            {
                if (!next.IsNext(previous))
                {
                    OnAggregateConflictDetected(aggregate, events, next, previous);

                    return false;
                }
            }

            return true;
        }

        protected void OnAggregateConflictDetected(
            Reference aggregate,
            IEnumerable<DomainEvent> events,
            SignedVersion next,
            SignedVersion previous)
        {
            AggregateConflictDetected?.Invoke(this, new AggregateConflictDetectedEventArgs(aggregate, events, next, previous));
        }

        protected void OnAggregateReconciled(Reference aggregate, IEnumerable<DomainEvent> events)
        {
            AggregateReconciled?.Invoke(this, new AggregateReconciledEventArgs(aggregate, events));
        }

        protected void OnUnsupportedAggregateDetected(Reference aggregate, IEnumerable<DomainEvent> events)
        {
            UnsupportedAggregateDetected?.Invoke(this, new UnsupportedAggregateDetectedEventArgs(aggregate, events));
        }

        protected virtual IEnumerable<DomainEvent> RemovePreviousVersions(IEnumerable<DomainEvent> events, SignedVersion version)
        {
            return events
                .Where(@event => @event.Aggregate.Version.CompareTo(version) > 0)
                .ToArray();
        }

        protected virtual void Apply(
            EventCentricAggregateRoot aggregate,
            IEnumerable<DomainEvent> events,
            IAggregateReconciliationProxy proxy,
            Reference reference)
        {
            if (events.Any())
            {
                try
                {
                    aggregate.LoadFromHistory(events);

                    proxy.Save(aggregate);

                    OnAggregateReconciled(reference, events);
                }
                catch (AggregateHistoryInvalidForStateException invalid)
                {
                    OnAggregateConflictDetected(
                        invalid.Aggregate,
                        events,
                        invalid.StartingVersion,
                        invalid.Aggregate.Version);
                }
                catch (AggregateConflictDetectedException conflict)
                {
                    OnAggregateConflictDetected(
                        reference,
                        events,
                        conflict.ReceivedVersion,
                        conflict.PersistedVersion);
                }
            }
        }
    }
}