namespace MooVC.Architecture.Ddd
{
    using System;

    internal sealed class DerivedAggregateRoot
        : SerializableAggregateRoot
    {
        public DerivedAggregateRoot(Guid id)
            : base(id)
        {
        }
    }
}