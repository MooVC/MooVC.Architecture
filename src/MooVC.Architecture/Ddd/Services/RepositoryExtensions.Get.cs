namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static MooVC.Architecture.Ddd.Ensure;

    public static partial class RepositoryExtensions
    {
        public static TAggregate Get<TAggregate>(
            this IRepository<TAggregate> repository,
            Message context,
            Guid id,
            SignedVersion? version = default)
            where TAggregate : AggregateRoot
        {
            TAggregate? aggregate = repository.Get(id, version: version);

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

        public static TAggregate Get<TAggregate>(
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

            return repository.Get(context, reference.Id);
        }

        public static TAggregate Get<TAggregate>(
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

            return repository.Get(context, reference.Id, version: reference.Version);
        }
    }
}