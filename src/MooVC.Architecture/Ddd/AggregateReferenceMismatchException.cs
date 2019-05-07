namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public sealed class AggregateReferenceMismatchException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        internal AggregateReferenceMismatchException(IReference reference)
            : base(string.Format(
                Resources.AggregateReferenceMismatchExceptionMessage,
                reference.Id,
                reference.Type.Name,
                typeof(TAggregate).Name))
        {
            Reference = reference;
        }

        public IReference Reference { get; }
    }
}