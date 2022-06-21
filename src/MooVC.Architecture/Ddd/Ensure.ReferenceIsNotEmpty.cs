namespace MooVC.Architecture.Ddd;

using System;
using System.Diagnostics.CodeAnalysis;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;

public static partial class Ensure
{
    public static T ReferenceIsNotEmpty<T>([NotNull] T? reference, string argumentName)
        where T : Reference
    {
        return ReferenceIsNotEmpty(reference, argumentName, Format(EnsureReferenceIsNotEmptyMessage, reference?.Type.Name));
    }

    public static T ReferenceIsNotEmpty<T>([NotNull] T? reference, string argumentName, string message)
        where T : Reference
    {
        if (reference is null || reference.IsEmpty)
        {
            throw new ArgumentException(message, argumentName);
        }

        return reference;
    }
}