namespace MooVC.Architecture.Ddd;

using Ardalis.GuardClauses;
using MooVC.Architecture.Cqrs;
using static MooVC.Architecture.Ddd.AggregateDoesNotExistException_Resources;

public sealed class AggregateDoesNotExistException<TAggregate>
    : ArgumentException
    where TAggregate : AggregateRoot
{
    public AggregateDoesNotExistException(Message context)
        : base(AggregateDoesNotExistMessage.Format(typeof(TAggregate)))
    {
        Context = Guard.Against.Null(context, message: ContextRequired);
    }

    public Message Context { get; }
}