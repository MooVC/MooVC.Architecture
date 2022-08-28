namespace MooVC.Architecture.Ddd;

using System;
using System.Diagnostics.CodeAnalysis;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;

public static partial class Ensure
{
    public static Reference<TAggregate> ReferenceIsOfType<TAggregate>(Reference? reference, string argumentName)
        where TAggregate : AggregateRoot
    {
        return ReferenceIsOfType<TAggregate>(
            reference,
            argumentName,
            Format(EnsureReferenceIsOfTypeMessage, reference?.Type.Name, typeof(TAggregate).Name));
    }

    public static Reference<TAggregate> ReferenceIsOfType<TAggregate>(Reference? reference, string argumentName, string message)
        where TAggregate : AggregateRoot
    {
        if (!reference.Is(out Reference<TAggregate>? aggregate))
        {
            throw new ArgumentException(message, argumentName);
        }

        return aggregate;
    }
}