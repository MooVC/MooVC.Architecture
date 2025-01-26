namespace MooVC.Architecture.Cqrs.Services;

/// <summary>
/// Defines a generic interface for handling messages in accordance with the CQRS (Command Query Responsibility Segregation) architectural style.
/// </summary>
/// <typeparam name="T">
/// The type of message to be handled, where T must be a subclass of <see cref="Message"/>.
/// </typeparam>
public interface IHandler<T>
    where T : Message
{
    /// <summary>
    /// Asynchronously handles the request associated with the specified message.
    /// </summary>
    /// <param name="message">
    /// The message of type <typeparamref name="T"/> to be handled.
    /// </param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete, enabling the operation to be canceled.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, containing no result upon completion.
    /// </returns>
    Task Execute(T message, CancellationToken cancellationToken);
}