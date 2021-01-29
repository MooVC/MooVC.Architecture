namespace MooVC.Architecture
{
    using System;
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    public static partial class MessageExtensions
    {
        public static void Coordinate(this Message message, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(message, nameof(message), MessageExtensionsCoordinateMessageRequired);

            message.GetType().Coordinate(
                message.Id,
                operation,
                timeout: timeout);
        }
    }
}