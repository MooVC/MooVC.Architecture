namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using System;

internal static class TestableAggregate
{
    public sealed class One
        : AggregateRoot
    {
        public One()
            : base(Guid.NewGuid())
        {
        }
    }

    public sealed class Two
        : AggregateRoot
    {
        public Two()
            : base(Guid.NewGuid())
        {
        }
    }

    public sealed class Three
        : AggregateRoot
    {
        public Three()
            : base(Guid.NewGuid())
        {
        }
    }

    public sealed class Four
        : AggregateRoot
    {
        public Four()
            : base(Guid.NewGuid())
        {
        }
    }

    public sealed class Five
        : AggregateRoot
    {
        public Five()
            : base(Guid.NewGuid())
        {
        }
    }
}