﻿namespace MooVC.Architecture.Ddd;

using System;

public static partial class GuidExtensions
{
    public static Reference ToReference(this Guid id, Type type, SignedVersion? version = default)
    {
        return Reference.Create(id, type, version: version);
    }

    public static Reference<TAggregate> ToReference<TAggregate>(this Guid id, SignedVersion? version = default)
        where TAggregate : AggregateRoot
    {
        return Reference<TAggregate>.Create(id, version: version);
    }
}