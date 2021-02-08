namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;

    public sealed class SetRequest
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