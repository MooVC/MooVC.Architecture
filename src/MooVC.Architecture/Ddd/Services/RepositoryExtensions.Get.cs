namespace MooVC.Architecture.Ddd.Services
{
    using System;

    public static partial class RepositoryExtensions
    {
        public static TAggregate Get<TAggregate>(
            this IRepository<TAggregate> repository, 
            Message context, 
            Guid id)
            where TAggregate : AggregateRoot
        {
            TAggregate aggregate = repository.Get(id);

            if (aggregate == null)
            {
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
            return repository.Get(context, reference.Id);
        }
    }
}