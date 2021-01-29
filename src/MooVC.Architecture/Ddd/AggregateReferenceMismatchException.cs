namespace MooVC.Architecture.Ddd
{
    using System;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    [Serializable]
    public sealed class AggregateReferenceMismatchException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateReferenceMismatchException(Reference reference)
            : base(Format(
                AggregateReferenceMismatchExceptionMessage,
                reference.Id,
                reference.Type.Name,
                typeof(TAggregate).Name))
        {
            Reference = reference;
        }

        public Reference Reference { get; }
    }
}