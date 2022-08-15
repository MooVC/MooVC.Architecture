namespace MooVC.Architecture.Cqrs.Services;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using MooVC.Serialization;
using static MooVC.Architecture.Cqrs.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class Result<TQuery, T>
    : Message
    where TQuery : Message
    where T : notnull
{
    protected Result(TQuery query, T value)
    {
        Query = ArgumentNotNull(query, nameof(query), ResultQueryRequired);
        Value = value;
    }

    protected Result(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Query = info.GetValue<TQuery>(nameof(Query));
        Value = info.GetValue<T>(nameof(Value));
    }

    public override Guid CausationId => Query.Id;

    public override Guid CorrelationId => Query.CorrelationId;

    public TQuery Query { get; }

    public T Value { get; }

    [return: NotNullIfNotNull("result")]
    public static implicit operator TQuery?(Result<TQuery, T>? result)
    {
        if (result is null)
        {
            return default;
        }

        return result.Query;
    }

    [return: NotNullIfNotNull("result")]
    public static implicit operator T?(Result<TQuery, T>? result)
    {
        if (result is null)
        {
            return default;
        }

        return result.Value;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(Query), Query);
        info.AddValue(nameof(Value), Value);
    }

    protected override Guid DeserializeCausationId(SerializationInfo info, StreamingContext context)
    {
        return Guid.Empty;
    }

    protected override Guid DeserializeCorrelationId(SerializationInfo info, StreamingContext context)
    {
        return Guid.Empty;
    }

    protected override void SerializeCausationId(SerializationInfo info, StreamingContext context)
    {
    }

    protected override void SerializeCorrelationId(SerializationInfo info, StreamingContext context)
    {
    }
}