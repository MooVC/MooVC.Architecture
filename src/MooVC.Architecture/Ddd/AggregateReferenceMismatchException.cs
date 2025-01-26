namespace MooVC.Architecture.Ddd;

using System;
using Ardalis.GuardClauses;
using static MooVC.Architecture.Ddd.Resources;

public sealed class AggregateReferenceMismatchException<TAggregate>
    : ArgumentException
    where TAggregate : AggregateRoot
{
    public AggregateReferenceMismatchException(Reference reference)
        : base(FormatMessage(reference))
    {
        Reference = reference;
    }

    public Reference Reference { get; }

    private static string FormatMessage(Reference reference)
    {
        _ = Guard.Against.Null(reference, message: AggregateReferenceMismatchExceptionReferenceRequired);

        return AggregateReferenceMismatchExceptionMessage.Format(reference.Id, reference.Type.Name, typeof(TAggregate).Name);
    }
}