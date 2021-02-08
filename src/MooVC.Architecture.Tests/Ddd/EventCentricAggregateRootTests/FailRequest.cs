namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    internal sealed class FailRequest
        : Request
    {
        public FailRequest(Message context)
            : base(context)
        {
        }
    }
}