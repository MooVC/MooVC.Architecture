namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;

    internal sealed class SetRequest
        : Request
    {
        public SetRequest(Message context, Guid value)
            : base(context)
        {
            Value = value;
        }

        public Guid Value { get; }
    }
}