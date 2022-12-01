namespace MooVC.Architecture.Cqrs.Services.PaginatedQueryTests;

using System;
using System.Runtime.Serialization;
using MooVC.Linq;

[Serializable]
internal sealed class SerializablePaginatedQuery
    : PaginatedQuery
{
    public SerializablePaginatedQuery(Paging? paging = default)
        : base(paging: paging)
    {
    }

    public SerializablePaginatedQuery(Message context, Paging? paging = default)
        : base(context, paging: paging)
    {
    }

    private SerializablePaginatedQuery(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}