namespace MooVC.Architecture
{
    using System;
    using MooVC.Architecture.Ddd;

    public static partial class Ensure
    {
        public static void ReferenceIsVersionSpecific<TAggregate>(Reference<TAggregate> reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            ReferenceIsVersionSpecific(
                reference, 
                argumentName, 
                string.Format(Resources.VersionSpecificReferenceRequired, typeof(TAggregate).Name));
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