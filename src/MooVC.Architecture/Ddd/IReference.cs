namespace MooVC.Architecture.Ddd
{
    using System;

    public interface IReference
    {
        bool IsEmpty { get; }

        Guid Id { get; }

        Type Type { get; }

        ulong Version { get; }

        bool IsMatch(AggregateRoot aggregate);
    }
}