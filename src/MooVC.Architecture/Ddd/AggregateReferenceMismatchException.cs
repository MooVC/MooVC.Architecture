namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
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

        private AggregateReferenceMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Reference = info.TryGetReference(nameof(Reference));
        }

        public Reference Reference { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddReference(nameof(Reference), Reference);
        }
    }
}