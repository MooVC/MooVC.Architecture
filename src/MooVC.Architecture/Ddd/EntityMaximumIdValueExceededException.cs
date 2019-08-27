namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public sealed class EntityMaximumIdValueExceededException
        : InvalidOperationException
    {
        public EntityMaximumIdValueExceededException(ulong max, Type type)
            : base(string.Format(
                Resources.EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }

        public EntityMaximumIdValueExceededException(long max, Type type)
            : base(string.Format(
                Resources.EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }
    }
}