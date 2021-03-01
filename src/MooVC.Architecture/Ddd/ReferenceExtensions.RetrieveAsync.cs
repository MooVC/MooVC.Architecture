﻿namespace MooVC.Architecture.Ddd
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Collections.Generic;

    public static partial class ReferenceExtensions
    {
        public static Task<TAggregate> RetrieveAsync<TAggregate>(
            this Reference reference,
            Message context,
            IRepository<TAggregate> repository)
            where TAggregate : AggregateRoot
        {
            return repository.GetAsync(context, reference);
        }

        public static Task<IEnumerable<TAggregate>> RetrieveAsync<TAggregate>(
            this IEnumerable<Reference> references,
            Message context,
            IRepository<TAggregate> repository,
            bool ignoreEmpty = false)
            where TAggregate : AggregateRoot
        {
            return references
                .Where(reference => !(ignoreEmpty && reference.IsEmpty))
                .ProcessAllAsync(reference => reference.RetrieveAsync(context, repository));
        }
    }
}