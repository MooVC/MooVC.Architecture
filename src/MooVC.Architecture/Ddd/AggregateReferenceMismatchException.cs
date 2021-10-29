namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class AggregateReferenceMismatchException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateReferenceMismatchException(Reference reference)
            : base(FormatMessage(reference))
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

        private static string FormatMessage(Reference reference)
        {
            _ = ArgumentNotNull(
                reference,
                nameof(reference),
                AggregateReferenceMismatchExceptionReferenceRequired);

            return Format(
                AggregateReferenceMismatchExceptionMessage,
                reference.Id,
                reference.Type.Name,
                typeof(TAggregate).Name);
        }
    }
}