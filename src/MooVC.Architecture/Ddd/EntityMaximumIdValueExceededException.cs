namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public sealed class EntityMaximumIdValueExceededException
        : InvalidOperationException
    {
        internal EntityMaximumIdValueExceededException(ulong max, Type type)
            : base(string.Format(
                Resources.EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }

        internal EntityMaximumIdValueExceededException(long max, Type type)
            : base(string.Format(
                Resources.EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name))
        {
        }
    }
}