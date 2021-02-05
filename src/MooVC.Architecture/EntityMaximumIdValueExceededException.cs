namespace MooVC.Architecture
{
    using System;
    using System.Runtime.Serialization;
    using static System.String;
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class EntityMaximumIdValueExceededException
        : InvalidOperationException
    {
        public EntityMaximumIdValueExceededException(ulong max, Type type)
            : base(FormatMessage(max.ToString(), type))
        {
        }

        public EntityMaximumIdValueExceededException(long max, Type type)
            : base(FormatMessage(max.ToString(), type))
        {
        }

        private EntityMaximumIdValueExceededException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string FormatMessage(string max, Type type)
        {
            ArgumentNotNull(type, nameof(type), EntityMaximumIdValueExceededExceptionTypeRequired);

            return Format(
                EntityMaximumIdValueExceededExceptionMessage,
                max,
                type.Name);
        }
    }
}