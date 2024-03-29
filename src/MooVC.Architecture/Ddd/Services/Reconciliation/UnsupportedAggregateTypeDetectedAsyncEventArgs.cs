﻿namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Threading;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

public sealed class UnsupportedAggregateTypeDetectedAsyncEventArgs
    : AsyncEventArgs
{
    internal UnsupportedAggregateTypeDetectedAsyncEventArgs(Type type, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Type = IsNotNull(type, message: UnsupportedAggregateTypeDetectedEventArgsTypeRequired);
    }

    public Type Type { get; }
}