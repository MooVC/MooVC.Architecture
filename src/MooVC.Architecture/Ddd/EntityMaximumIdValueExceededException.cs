namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class EntityMaximumIdValueExceededException
        : InvalidOperationException
    {
        public EntityMaximumIdValueExceededException(ulong max, Type type)
            : base(string.Format(
                EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }

        public EntityMaximumIdValueExceededException(long max, Type type)
            : base(string.Format(
                EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }
    }
}