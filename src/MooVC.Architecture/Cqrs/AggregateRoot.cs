namespace MooVC.Architecture.Cqrs
{
    using System;
    using System.Collections.Generic;
    using Collections.Generic;
    using Ddd;
    using AggregateRootBase = Ddd.AggregateRoot;

    public abstract class AggregateRoot
        : AggregateRootBase
    {
        private readonly List<DomainEvent> changes = new List<DomainEvent>();

        protected AggregateRoot(Guid id)
            : base(id)
        {
        }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return changes;
        }

        public void LoadFromHistory(IEnumerable<DomainEvent> history)
        {
            history.ForEach(@event => ApplyChange(@event, isNew: false));
        }

        public void MarkChangesAsCommitted()
        {
            changes.Clear();
        }

        protected void ApplyChange(DomainEvent @event, bool isNew = true)
        {
            dynamic aggregate = (dynamic)this;

            _ = aggregate.Handle(@event);

            if (isNew)
            {
                changes.Add(@event);
            }
        }
    }
}