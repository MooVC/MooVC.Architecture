namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Ddd.Ensure;

    public static partial class RepositoryExtensions
    {
        public static async Task<TAggregate> GetAsync<TAggregate>(
            this IRepository<TAggregate> repository,
            Message context,
            Guid id,
            SignedVersion? version = default)
            where TAggregate : AggregateRoot
        {
            TAggregate? aggregate = await repository
                .GetAsync(id, version: version)
                .ConfigureAwait(false);

            if (aggregate is null)
            {
                if (version is { })
                {
                    throw new AggregateVersionNotFoundException<TAggregate>(context, id, version);
                }

                throw new AggregateNotFoundException<TAggregate>(context, id);
            }

            return aggregate;
        }

        public static Task<TAggregate> GetAsync<TAggregate>(
            this IRepository<TAggregate> repository,
            Message context,
            Reference reference)
            where TAggregate : AggregateRoot
        {
            if (reference.IsEmpty)
            {
                throw new AggregateDoesNotExistException<TAggregate>(context);
            }

            ReferenceIsOfType<TAggregate>(reference, nameof(reference));

            return repository.GetAsync(context, reference.Id);
        }

        public static Task<TAggregate> GetAsync<TAggregate>(
            this IRepository<TAggregate> repository,
            Message context,
            VersionedReference reference)
            where TAggregate : AggregateRoot
        {
            if (reference.IsEmpty)
            {
                throw new AggregateDoesNotExistException<TAggregate>(context);
            }

            ReferenceIsOfType<TAggregate>(reference, nameof(reference));

            return repository.GetAsync(context, reference.Id, version: reference.Version);
        }
    }
}