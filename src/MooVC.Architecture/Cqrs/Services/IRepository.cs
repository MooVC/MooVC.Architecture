namespace MooVC.Architecture.Cqrs.Services
{
    public interface IRepository<TAggregate>
        : Ddd.Services.IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        void Save(TAggregate aggregate);
    }
}