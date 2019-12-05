namespace MooVC.Architecture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd;
    using MooVC.Collections.Generic;

    public static partial class RequestExtensions
    {
        public static void Satisfies<T>(
            this T request,
            AggregateRoot aggregate,
            params (Func<T, bool> IsSatisfied, string Explaination)[] invariants)
            where T : Request
        {
            request.Satisfies(
                aggregate.ToVersionedReference(),
                invariants);
        }

        public static void Satisfies<T>(
            this T request,
            VersionedReference aggregate,
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