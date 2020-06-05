namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;

    public interface IAggregateReconciliationProxy
    {
        EventCentricAggregateRoot Get(Reference aggregate);

        IEnumerable<EventCentricAggregateRoot> GetAll();

        EventCentricAggregateRoot Create(Reference aggregate);

        void Purge(Reference aggregate);

        void Save(EventCentricAggregateRoot aggregate);

        void Overwrite(EventCentricAggregateRoot aggregate);
    }
}