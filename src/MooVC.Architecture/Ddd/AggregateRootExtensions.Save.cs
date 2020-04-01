namespace MooVC.Architecture.Ddd
{
    using MooVC.Architecture.Ddd.Services;

    public static partial class AggregateRootExtensions
    {
        public static void Save<TAggregate>(this TAggregate aggregate, IRepository<TAggregate> repository)
            where TAggregate : AggregateRoot
        {
            if (aggregate is { } && aggregate.HasUncommittedChanges)
            {
                repository.Save(aggregate);
            }
        }
    }
}