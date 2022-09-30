namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.CompilerServices;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;

public static partial class Ensure
{
    public static Reference<TAggregate> IsOfType<TAggregate>(
        Reference? reference,
        [CallerArgumentExpression("reference")] string? argumentName = default,
        Reference<TAggregate>? @default = default,
        string? message = default)
        where TAggregate : AggregateRoot
    {
        if (reference.Is(out Reference<TAggregate>? aggregate))
        {
            return aggregate;
        }

        if (@default is null)
        {
            message ??= Format(EnsureReferenceIsOfTypeMessage, reference?.Type.Name, typeof(TAggregate).Name);

            throw new ArgumentException(message, argumentName);
        }

        return @default;
    }
}