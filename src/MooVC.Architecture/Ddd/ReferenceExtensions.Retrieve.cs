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
            this IReference reference, 
            Message context, 
            IRepository<TAggregate> repository,
            bool getLatest = false)
            where TAggregate : AggregateRoot
        {
            return repository.Get(context, reference, getLatest: getLatest);
        }

        public static IEnumerable<TAggregate> Retrieve<TAggregate>(
            this IEnumerable<IReference> references,
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