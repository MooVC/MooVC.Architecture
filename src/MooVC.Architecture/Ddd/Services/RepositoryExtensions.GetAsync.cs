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
                throw new AggregateVersionNotFoundException<TAggregate>(context, id, version: version);
            }

            return aggregate;
        }

        public static Task<TAggregate> GetAsync<TAggregate>(
            this IRepository<TAggregate> repository,
            Message context,
            Reference reference,
            bool latest = true)
            where TAggregate : AggregateRoot
        {
            if (reference.IsEmpty)
            {
                throw new AggregateDoesNotExistException<TAggregate>(context);
            }

            ReferenceIsOfType<TAggregate>(reference, nameof(reference));

            if (latest || reference.Version.IsEmpty)
            {
                return repository.GetAsync(context, reference.Id);
            }

            return repository.GetAsync(context, reference.Id, version: reference.Version);
        }
    }
}