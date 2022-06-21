namespace MooVC.Architecture.Ddd;

public sealed class FailRequest
    : Request
{
    public FailRequest(Message context)
        : base(context)
    {
    }
}