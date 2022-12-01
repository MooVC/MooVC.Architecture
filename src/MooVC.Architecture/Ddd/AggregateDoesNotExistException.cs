namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using MooVC.Serialization;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class AggregateDoesNotExistException<TAggregate>
    : ArgumentException
    where TAggregate : AggregateRoot
{
    public AggregateDoesNotExistException(Message context)
        : base(Format(AggregateDoesNotExistExceptionMessage, typeof(TAggregate).Name))
    {
        Context = IsNotNull(context, message: AggregateDoesNotExistExceptionContextRequired);
    }

    private AggregateDoesNotExistException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Context = info.GetValue<Message>(nameof(Context));
    }

    public Message Context { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(Context), Context);
    }
}