namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

public static partial class RepositoryExtensions
{
    public static async Task<TAggregate> GetAsync<TAggregate>(
        this IRepository<TAggregate> repository,
        Message context,
        Guid id,
        CancellationToken? cancellationToken = default,
        Sequence? version = default)
        where TAggregate : AggregateRoot
    {
        TAggregate? aggregate = await repository
            .GetAsync(id, cancellationToken: cancellationToken, version: version)
            .ConfigureAwait(false);

        if (aggregate is null)
        {
            if (version is null || version.IsEmpty)
            {
                throw new AggregateNotFoundException<TAggregate>(id, context);
            }

            throw new AggregateVersionNotFoundException<TAggregate>(id, context, version: version);
        }

        return aggregate;
    }

    public static Task<TAggregate> GetAsync<TAggregate>(
        this IRepository<TAggregate> repository,
        Message context,
        Reference reference,
        CancellationToken? cancellationToken = default,
        bool latest = true)
        where TAggregate : AggregateRoot
    {
        if (reference.IsEmpty || !reference.Is<TAggregate>(out _))
        {
            throw new AggregateDoesNotExistException<TAggregate>(context);
        }

        if (latest || reference.Version.IsEmpty)
        {
            return repository.GetAsync(context, reference.Id, cancellationToken: cancellationToken);
        }

        return repository.GetAsync(context, reference.Id, cancellationToken: cancellationToken, version: reference.Version);
    }
}