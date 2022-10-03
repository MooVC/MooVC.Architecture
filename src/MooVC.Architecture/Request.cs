namespace MooVC.Architecture;

using static MooVC.Architecture.Resources;
using static MooVC.Ensure;

public abstract class Request
{
    protected Request(Message context)
    {
        Context = IsNotNull(context, message: RequestContextRequired);
    }

    public Message Context { get; }

    public static implicit operator Message(Request request)
    {
        return request.Context;
    }

    public override string ToString()
    {
        return Context.ToString();
    }
}