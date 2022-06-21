namespace MooVC.Architecture;

using static MooVC.Architecture.Resources;
using static MooVC.Ensure;

public abstract class Request
{
    protected Request(Message context)
    {
        Context = ArgumentNotNull(context, nameof(context), RequestContextRequired);
    }

    public Message Context { get; }

    public static implicit operator Message(Request request)
    {
        return request.Context;
    }
}