namespace MooVC.Architecture.Ddd
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Collections.Generic;

    public static partial class ReferenceExtensions
    {
        public static TAggregate Retrieve<TAggregate>(
            this Reference reference, 
            Message context, 
            IRepository<TAggregate> repository)
            where TAggregate : AggregateRoot
        {
            return repository.Get(context, reference);
        }

        public static IEnumerable<TAggregate> Retrieve<TAggregate>(
            this IEnumerable<Reference> references,
            Message context,
            IRepository<TAggregate> repository,
            bool ignoreEmpty = false)
            where TAggregate : AggregateRoot
        {
            var aggregates = new ConcurrentBag<TAggregate>();

            references
                .Where(reference => !(ignoreEmpty && reference.IsEmpty))
                .ForAll(reference => aggregates.Add(reference.Retrieve(context, repository)));

            return aggregates.ToArray();
        }
    }
}