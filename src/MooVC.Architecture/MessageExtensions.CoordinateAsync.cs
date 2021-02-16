namespace MooVC.Architecture
{
    using System;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    public static partial class MessageExtensions
    {
        public static async Task CoordinateAsync(this Message message, Func<Task> operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(message, nameof(message), MessageExtensionsCoordinateMessageRequired);

            await message
                .GetType()
                .CoordinateAsync(
                    message.Id,
                    operation,
                    timeout: timeout);
        }
    }
}