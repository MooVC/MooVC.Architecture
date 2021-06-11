namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests
{
    using System;
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd;

    public sealed class TestableSynchronousAggregateReconciliationProxy
        : SynchronousAggregateReconciliationProxy
    {
        private readonly Func<Reference, EventCentricAggregateRoot?>? get;
        private readonly Func<IEnumerable<EventCentricAggregateRoot>>? getAll;
        private readonly Action<EventCentricAggregateRoot>? overwrite;
        private readonly Action<Reference>? purge;
        private readonly Action<EventCentricAggregateRoot>? save;

        public TestableSynchronousAggregateReconciliationProxy(
            Func<Reference, EventCentricAggregateRoot?>? get = default,
            Func<IEnumerable<EventCentricAggregateRoot>>? getAll = default,
            Action<EventCentricAggregateRoot>? overwrite = default,
            Action<Reference>? purge = default,
            Action<EventCentricAggregateRoot>? save = default)
        {
            this.get = get;
            this.getAll = getAll;
            this.overwrite = overwrite;
            this.purge = purge;
            this.save = save;
        }

        protected override EventCentricAggregateRoot? PerformGet(Reference aggregate)
        {
            if (get is null)
            {
                throw new NotImplementedException();
            }

            return get(aggregate);
        }

        protected override IEnumerable<EventCentricAggregateRoot> PerformGetAll()
        {
            if (getAll is null)
            {
                throw new NotImplementedException();
            }

            return getAll();
        }

        protected override void PerformOverwrite(EventCentricAggregateRoot aggregate)
        {
            if (overwrite is null)
            {
                throw new NotImplementedException();
            }

            overwrite(aggregate);
        }

        protected override void PerformPurge(Reference aggregate)
        {
            if (purge is null)
            {
                throw new NotImplementedException();
            }

            purge(aggregate);
        }

        protected override void PerformSave(EventCentricAggregateRoot aggregate)
        {
            if (save is null)
            {
                throw new NotImplementedException();
            }

            save(aggregate);
        }
    }
}