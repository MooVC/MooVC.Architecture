namespace MooVC.Architecture.RequestTests
{
    internal sealed class TestableRequest
        : Request
    {
        public TestableRequest(Message context) 
            : base(context)
        {
        }
    }
}