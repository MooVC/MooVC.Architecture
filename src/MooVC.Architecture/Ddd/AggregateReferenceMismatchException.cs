namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class AggregateReferenceMismatchException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateReferenceMismatchException(Reference reference)
            : base(string.Format(
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