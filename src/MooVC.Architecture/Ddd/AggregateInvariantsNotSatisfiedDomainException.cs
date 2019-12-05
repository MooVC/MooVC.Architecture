namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using static System.String;
    using static Resources;

    [Serializable]
    public sealed class AggregateInvariantsNotSatisfiedDomainException
        : DomainException
    {
        private const string Separator = "\r\n";

        public AggregateInvariantsNotSatisfiedDomainException(
            Request request,
            AggregateRoot aggregate,
            IEnumerable<string> explainations)
            : this(request, aggregate.ToVersionedReference(), explainations)
        {
        }

        public AggregateInvariantsNotSatisfiedDomainException(
            Request request,
            VersionedReference aggregate,
            IEnumerable<string> explainations)
            : base(
                  request.Context,
                  aggregate,
                  Format(
                      AggregateInvariantsNotSatisfiedDomainExceptionMessage,
                      request.GetType().Name,
                      aggregate.Type.Name,
                      Join(Separator, explainations)))
        {
        }
    }
}