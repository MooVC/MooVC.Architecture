namespace MooVC.Architecture.Cqrs.Services.ResultTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class SerializableResult<T>
    : Result<T>
    where T : notnull
{
    public SerializableResult(Message context, T value)
        : base(context, value)
    {
    }

    private SerializableResult(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}