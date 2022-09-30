namespace MooVC.Architecture.Ddd;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;

public static partial class Ensure
{
    public static T IsNotEmpty<T>(
        T? reference,
        [CallerArgumentExpression("reference")] string? argumentName = default,
        T? @default = default,
        string? message = default)
        where T : Reference
    {
        if (reference is null || reference.IsEmpty)
        {
            if (@default is null)
            {
                message ??= Format(EnsureReferenceIsNotEmptyMessage, reference?.Type.Name);

                throw new ArgumentException(message, argumentName);
            }

            return @default;
        }

        return reference;
    }
}