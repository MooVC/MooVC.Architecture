namespace MooVC.Architecture.Ddd
{
    using System;

    public static partial class GuidExtensions
    {
        public static Reference<TAggregate> ToReference<TAggregate>(this Guid id)
            where TAggregate : AggregateRoot
        {
            return id == Guid.Empty
                ? Reference<TAggregate>.Empty
                : new Reference<TAggregate>(id);
        }
    }
}