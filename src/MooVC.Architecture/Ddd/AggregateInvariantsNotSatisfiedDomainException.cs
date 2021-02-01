namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using static System.Environment;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    [Serializable]
    public sealed class AggregateInvariantsNotSatisfiedDomainException
        : DomainException
    {
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
                      Join(NewLine, explainations)))
        {
        }
    }
}