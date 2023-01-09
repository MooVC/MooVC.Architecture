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
        Query = IsNotNull(query, message: ResultQueryRequired);
        Value = value;
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
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