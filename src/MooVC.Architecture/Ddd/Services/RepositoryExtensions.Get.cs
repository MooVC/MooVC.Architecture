namespace MooVC.Architecture.Ddd.Services
{
    using System;

    public static partial class RepositoryExtensions
    {
        public static TAggregate Get<TAggregate>(
            this IRepository<TAggregate> repository, 
            Message context, 
            Guid id,
            ulong? version = null)
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
            Reference<TAggregate> reference)
            where TAggregate : AggregateRoot
        {
            return repository.Get(context, reference.Id, version: reference.Version);
        }
    }
}