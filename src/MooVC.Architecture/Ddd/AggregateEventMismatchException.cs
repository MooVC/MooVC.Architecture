namespace MooVC.Architecture.Ddd;

using Ardalis.GuardClauses;
using static System.String;
using static MooVC.Architecture.Ddd.AggregateEventMismatchException_Resources;
using static MooVC.Architecture.Ddd.Reference;

public sealed class AggregateEventMismatchException
    : ArgumentException
{
    public AggregateEventMismatchException(AggregateRoot aggregate, Reference eventAggregate)
        : this(Create(aggregate), eventAggregate)
    {
    }

    public AggregateEventMismatchException(Reference aggregate, Reference eventAggregate)
        : base(FormatMessage(aggregate, eventAggregate))
    {
        Aggregate = aggregate;
        EventAggregate = eventAggregate;
    }

    public Reference Aggregate { get; }

    public Reference EventAggregate { get; }

    private static string FormatMessage(Reference aggregate, Reference eventAggregate)
    {
        _ = Guard.Against.Null(aggregate, message: FormatMessageAggregateRequired);
        _ = Guard.Against.Null(eventAggregate, message: FormatMessageEventAggregateRequired);

        return FormatMessageAggregateEventMismatch.Format(
            aggregate.Id,
            aggregate.Type.Name,
            aggregate.Version,
            eventAggregate.Id,
            eventAggregate.Type.Name,
            eventAggregate.Version);
    }
}