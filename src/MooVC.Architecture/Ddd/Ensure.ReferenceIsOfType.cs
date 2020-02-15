namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsOfType<TAggregate>(Reference reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            ReferenceIsOfType<TAggregate>(
                reference,
                argumentName,
                string.Format(EnsureReferenceIsOfTypeMessage, reference?.Type.Name, typeof(TAggregate).Name));
        }

        public static void ReferenceIsOfType<TAggregate>(Reference reference, string argumentName, string message)
            where TAggregate : AggregateRoot
        {
            if (reference is null || reference.Type != typeof(TAggregate))
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}