namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;

    internal sealed class DerivedAggregateRoot
        : SerializableAggregateRoot
    {
        public DerivedAggregateRoot(Guid id)
            : base(id)
        {
        }
    }
}