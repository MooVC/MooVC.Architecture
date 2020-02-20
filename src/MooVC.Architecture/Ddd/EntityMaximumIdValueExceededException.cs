namespace MooVC.Architecture.Ddd
{
    using System;
    using static System.String;
    using static Resources;

    [Serializable]
    public sealed class EntityMaximumIdValueExceededException
        : InvalidOperationException
    {
        public EntityMaximumIdValueExceededException(ulong max, Type type)
            : base(Format(
                EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }

        public EntityMaximumIdValueExceededException(long max, Type type)
            : base(Format(
                EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }
    }
}