namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    public sealed class FailRequest
        : Request
    {
        public FailRequest(Message context)
            : base(context)
        {
        }
    }
}