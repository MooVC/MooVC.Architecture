namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using static MooVC.Architecture.Ddd.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class Reference<TAggregate>
    : Reference
    where TAggregate : AggregateRoot
{
    private static readonly Lazy<Reference<TAggregate>> empty = new(() => new Reference<TAggregate>());

    internal Reference(TAggregate aggregate)
        : this(aggregate.Id, aggregate.GetType(), aggregate.Version)
    {
    }

    internal Reference(Guid id, Sequence? version = default)
        : this(id, typeof(TAggregate), version: version)
    {
    }

    internal Reference(Guid id, Type type, Sequence? version = default)
       : base(id, type, version: version)
    {
        _ = IsNotEmpty(id, message: ReferenceIdRequired);
        _ = Satisfies(type, value => !value.IsAbstract, message: ReferenceTypeRequired);
    }

    private Reference()
        : base(Guid.Empty, typeof(TAggregate), Sequence.Empty)
    {
    }

    private Reference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public static Reference<TAggregate> Empty => empty.Value;

    public static Reference<TAggregate> Create(Guid id, Sequence? version = default)
    {
        return (Reference<TAggregate>)Create(id, typeof(TAggregate), version: version);
    }

    protected override Type DeserializeType(SerializationInfo info, StreamingContext context)
    {
        return DeserializeType(typeof(TAggregate), info, context);
    }

    protected override void SerializeType(SerializationInfo info, StreamingContext context)
    {
        if (Type != typeof(TAggregate))
        {
            base.SerializeType(info, context);
        }
    }
}