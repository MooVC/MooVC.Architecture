﻿namespace MooVC.Architecture.Ddd;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Services;
using static MooVC.Architecture.Ddd.Resources;
using static MooVC.Ensure;

public static partial class AggregateRootExtensions
{
    public static async Task SaveAsync<TAggregate>(
        this TAggregate aggregate,
        IRepository<TAggregate> destination,
        CancellationToken? cancellationToken = default)
        where TAggregate : AggregateRoot
    {
        if (aggregate is { } && aggregate.HasUncommittedChanges)
        {
            _ = IsNotNull(destination, message: AggregateRootExtensionsSaveDestinationRequired);

            await destination
                .SaveAsync(aggregate, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}