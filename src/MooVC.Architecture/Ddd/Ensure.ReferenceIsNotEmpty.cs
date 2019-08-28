namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsNotEmpty<TAggregate>(Reference<TAggregate> reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            ReferenceIsNotEmpty(
                reference, 
                argumentName, 
                string.Format(EnsureReferenceIsNotEmptyMessage, typeof(TAggregate).Name));
        }

        public static void ReferenceIsNotEmpty<TAggregate>(Reference<TAggregate> reference, string argumentName, string message)
            where TAggregate : AggregateRoot
        {
            if (reference.IsEmpty)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}