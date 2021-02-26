namespace MooVC.Architecture
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    public static partial class MessageExtensions
    {
        public static async Task CoordinateAsync(
            this Message message,
            Func<Task> operation,
            CancellationToken? cancellationToken = default,
            TimeSpan? timeout = default)
        {
            ArgumentNotNull(message, nameof(message), MessageExtensionsCoordinateAsyncMessageRequired);

            await message
                .GetType()
                .CoordinateAsync(
                    message.Id,
                    operation,
                    cancellationToken: cancellationToken,
                    timeout: timeout);
        }
    }
}