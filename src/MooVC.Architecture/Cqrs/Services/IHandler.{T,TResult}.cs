namespace MooVC.Architecture.Cqrs.Services;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Defines a generic interface for handling messages in a accordance with the CQRS (Command Query Responsibility Segregation) architecture style that
/// expect a response message in return.
/// </summary>
/// <typeparam name="T">The type of the input message to be handled, where T must be a subclass of <see cref="Message"/>.</typeparam>
/// <typeparam name="TResult">The type of the result message produced by the handler, where TResult must be a subclass of <see cref="Message"/>.</typeparam>
public interface IHandler<T, TResult>
    where T : Message
    where TResult : Message
{
    /// <summary>
    /// Asynchronously executes the handling of the specified message of type <typeparamref name="T"/>, returning a message of type
    /// <typeparamref name="TResult"/> in response upon completion.
    /// </summary>
    /// <param name="message">The input message of type <typeparamref name="T"/> to be handled.</param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete, enabling the operation to be canceled.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, containing the result message of type <typeparamref name="TResult"/> upon completion.
    /// </returns>
    Task<TResult> ExecuteAsync(T message, CancellationToken cancellationToken);
}