namespace MooVC.Architecture
{
    using System;
    using MooVC.Architecture.Ddd;

    public static partial class Ensure
    {
        public static void ReferenceIsVersionSpecific<TAggregate>(Reference<TAggregate> reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            if (!reference.IsVersionSpecific)
            {
                throw new ArgumentException(string.Format(Resources.VersionSpecificReferenceRequired, typeof(TAggregate).Name), argumentName);
            }
        }
    }
}