namespace MooVC.Architecture.Cqrs.Services;

using System;
using System.Runtime.Serialization;
using MooVC.Linq;
using MooVC.Serialization;
using static MooVC.Architecture.Cqrs.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class PaginatedQuery
    : Message
{
    protected PaginatedQuery(Paging paging)
    {
        Paging = ArgumentNotNull(paging, nameof(paging), PaginatedQueryPagingRequired);
    }

    protected PaginatedQuery(Message context, Paging paging)
        : base(context)
    {
        Paging = ArgumentNotNull(paging, nameof(paging), PaginatedQueryPagingRequired);
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