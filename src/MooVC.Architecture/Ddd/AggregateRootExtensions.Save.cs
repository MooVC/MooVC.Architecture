namespace MooVC.Architecture.Ddd
{
    using MooVC.Architecture.Ddd.Services;
    using static MooVC.Ensure;
    using static MooVC.Architecture.Ddd.Resources;

    public static partial class AggregateRootExtensions
    {
        public static void Save<TAggregate>(this TAggregate aggregate, IRepository<TAggregate> destination)
            where TAggregate : AggregateRoot
        {
            if (aggregate is { } && aggregate.HasUncommittedChanges)
            {
                ArgumentNotNull(destination, nameof(destination), AggregateRootExtensionsSaveDestinationRequired);

                destination.Save(aggregate);
            }
        }
    }
}