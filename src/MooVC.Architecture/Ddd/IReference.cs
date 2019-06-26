namespace MooVC.Architecture.Ddd
{
    using System;

    public interface IReference
    {
        Guid Id { get; }

        bool IsEmpty { get; }

        bool IsVersionSpecific { get; }

        Type Type { get; }

        ulong? Version { get; }

        bool IsMatch<TAggregate>(TAggregate aggregate)
            where TAggregate : AggregateRoot;
    }
}