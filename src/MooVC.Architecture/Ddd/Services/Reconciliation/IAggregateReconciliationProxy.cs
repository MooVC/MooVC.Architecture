namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public interface IAggregateReconciliationProxy
    {
        EventCentricAggregateRoot Get(Reference aggregate);

        EventCentricAggregateRoot Create(Reference aggregate);

        void Purge(Reference aggregate);

        void Save(EventCentricAggregateRoot aggregate);

        void Overwrite(EventCentricAggregateRoot aggregate);
    }
}