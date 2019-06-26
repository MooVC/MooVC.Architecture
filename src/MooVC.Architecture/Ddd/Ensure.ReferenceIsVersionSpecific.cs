namespace MooVC.Architecture.Ddd
{
    using System;

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