namespace MooVC.Architecture
{
    using System;
    using MooVC.Architecture.Ddd;

    public static partial class Ensure
    {
        public static void ReferenceIsNotVersionSpecific<TAggregate>(Reference<TAggregate> reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            if (reference.IsVersionSpecific)
            {
                throw new ArgumentException(string.Format(Resources.NonVersionSpecificReferenceRequired, typeof(TAggregate).Name), argumentName);
            }
        }
    }
}