namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsVersionSpecific<TAggregate>(Reference<TAggregate> reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            ReferenceIsVersionSpecific(
                reference, 
                argumentName, 
                string.Format(VersionSpecificReferenceRequired, typeof(TAggregate).Name));
        }

        public static void ReferenceIsVersionSpecific<TAggregate>(Reference<TAggregate> reference, string argumentName, string message)
            where TAggregate : AggregateRoot
        {
            if (!reference.IsVersionSpecific)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}