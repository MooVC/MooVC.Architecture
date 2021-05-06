namespace MooVC.Architecture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd;
    using MooVC.Collections.Generic;

    public static partial class RequestExtensions
    {
        public static void Satisfies<TRequest, TAggregate>(
            this TRequest request,
            TAggregate aggregate,
            params (Func<TRequest, bool> IsSatisfied, string Explaination)[] invariants)
            where TRequest : Request
            where TAggregate : AggregateRoot
        {
            request.Satisfies(
                aggregate.ToReference(),
                invariants);
        }

        public static void Satisfies<T>(
            this T request,
            Reference aggregate,
            params (Func<T, bool> IsSatisfied, string Explaination)[] invariants)
            where T : Request
        {
            request.Satisfies(
                explainations => new AggregateInvariantsNotSatisfiedDomainException(request, aggregate, explainations),
                invariants);
        }

        public static void Satisfies<T>(
            this T request,
            Func<IEnumerable<string>, DomainException> factory,
            params (Func<T, bool> IsSatisfied, string Explaination)[] invariants)
            where T : Request
        {
            var explainations = new List<string>();

            invariants.ForEach(invariant =>
            {
                if (!invariant.IsSatisfied(request))
                {
                    explainations.Add(invariant.Explaination);
                }
            });

            if (explainations.Any())
            {
                throw factory(explainations);
            }
        }
    }
}