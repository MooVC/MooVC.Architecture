namespace MooVC.Architecture
{
    using System;
    using MooVC.Architecture.Ddd;

    public static partial class Ensure
    {
        public static void ReferenceIsNotEmpty<TAggregate>(Reference<TAggregate> reference, string argumentName)
            where TAggregate : AggregateRoot
        {
            if (reference.IsEmpty)
            {
                throw new ArgumentException(string.Format(Resources.NonEmptyReferenceRequired, typeof(TAggregate).Name), argumentName);
            }
        }
    }
}