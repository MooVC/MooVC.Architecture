namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;

    internal sealed class DerivedAggregateRoot
        : SerializableAggregateRoot
    {
        public DerivedAggregateRoot(Guid id, ulong version = 1)
            : base(id, version)
        {
        }
    }
}