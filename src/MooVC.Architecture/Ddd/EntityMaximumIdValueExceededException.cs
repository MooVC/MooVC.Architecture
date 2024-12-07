namespace MooVC.Architecture.Ddd;

using System;
using System.Globalization;
using Ardalis.GuardClauses;
using static System.String;
using static MooVC.Architecture.Ddd.EntityMaximumIdValueExceededException_Resources;

public sealed class EntityMaximumIdValueExceededException
    : InvalidOperationException
{
    public EntityMaximumIdValueExceededException(ulong max, Type type)
        : base(FormatMessage(max.ToString(), type))
    {
    }

    public EntityMaximumIdValueExceededException(long max, Type type)
        : base(FormatMessage(max.ToString(), type))
    {
    }

    private static string FormatMessage(string max, Type type)
    {
        _ = Guard.Against.Null(type, message: FormatMessageTypeRequired);

        return Format(CultureInfo.CurrentCulture, FormatMessageMessage, max, type.Name);
    }
}