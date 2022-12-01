namespace MooVC.Architecture.Cqrs.Services;

using System;
using System.Runtime.Serialization;
using MooVC.Linq;
using MooVC.Serialization;

[Serializable]
public abstract class PaginatedQuery
    : Message
{
    protected PaginatedQuery(Message? context = default, Paging? paging = default)
        : base(context: context)
    {
        Paging = paging ?? Paging.Default;
    }

    protected PaginatedQuery(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Paging = info.GetValue<Paging>(nameof(Paging));
    }

    public Paging Paging { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(Paging), Paging);
    }
}