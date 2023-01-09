namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Serialization;
using MooVC.Serialization;

[Serializable]
public abstract class DomainException
    : InvalidOperationException
{
    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    protected DomainException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Context = info.GetValue<Message>(nameof(Context));
        TimeStamp = info.GetValue<DateTimeOffset>(nameof(TimeStamp));
    }

    private protected DomainException(Reference aggregate, Message context, string message)
        : base(message)
    {
        Aggregate = aggregate;
        Context = context;
    }

    public Reference Aggregate { get; }

    public Message Context { get; }

    public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

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

        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
        info.AddValue(nameof(Context), Context);
        info.AddValue(nameof(TimeStamp), TimeStamp);
    }
}