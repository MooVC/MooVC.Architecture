namespace MooVC.Architecture.Cqrs.Services.EnumerableResultTests;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
internal sealed class SerializableEnumerableResult<T>
    : EnumerableResult<Message, T>
{
    public SerializableEnumerableResult(Message context, IEnumerable<T> results)
        : base(context, results)
    {
    }

    private SerializableEnumerableResult(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}