namespace MooVC.Architecture;

using Ardalis.GuardClauses;
using MooVC.Architecture.Cqrs;
using static MooVC.Architecture.Request_Resources;

public abstract class Request
{
    protected Request(Message context)
    {
        Context = Guard.Against.Null(context, message: ContextRequired);
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