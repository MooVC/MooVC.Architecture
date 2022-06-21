namespace MooVC.Architecture;

public abstract class Request<T>
    : Request
    where T : Message
{
    protected Request(T context)
        : base(context)
    {
    }

    public new T Context => (T)base.Context;

    public static implicit operator T(Request<T> request)
    {
        return request.Context;
    }
}