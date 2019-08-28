namespace MooVC.Architecture.Ddd
{
    using System;
    using static System.String;
    using static Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsVersionSpecific<TAggregate>(IReference reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            ReferenceIsVersionSpecific(
                reference,
                argumentName,
                Format(EnsureReferenceIsVersionSpecificMessage, reference?.Type.Name));
        }

        public static void ReferenceIsVersionSpecific<TAggregate>(Reference<TAggregate> reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            ReferenceIsVersionSpecific(
                reference,
                argumentName,
                Format(EnsureReferenceIsVersionSpecificMessage, typeof(TAggregate).Name));
        }

        public static void ReferenceIsVersionSpecific(IReference reference, string argumentName, string message)
        {
            if (!reference.IsVersionSpecific)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}