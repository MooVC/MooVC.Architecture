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
            ulong? version = default)
            where TAggregate : AggregateRoot
        {
            TAggregate aggregate = repository.Get(id, version: version);

            if (aggregate == null)
            {
                if (version.HasValue)
                {
                    throw new AggregateVersionNotFoundException<TAggregate>(context, id, version.Value);
                }

                throw new AggregateNotFoundException<TAggregate>(context, id);
            }

            return aggregate;
        }

        public static TAggregate Get<TAggregate>(
            this IRepository<TAggregate> repository, 
            Message context, 
            IReference reference,
            bool getLatest = false)
            where TAggregate : AggregateRoot
        {
            if (reference.IsEmpty)
            {
                throw new AggregateDoesNotExistException<TAggregate>(context);
            }

            ReferenceIsOfType<TAggregate>(reference, nameof(reference));

            return repository.Get(
                context, 
                reference.Id, 
                version: getLatest ? default(ulong?) : reference.Version);
        }
    }
}