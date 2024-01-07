namespace MooVC.Architecture.Services;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Cqrs;

/// <summary>
/// Defines a contract for encapsulating request-response interactions within a system, centralizing external communications
/// and reducing dependencies between the sending and receiving objects.
///
/// In a CQRS (Command Query Responsibility Segregation) context, it acts as a middleman for handling command and query objects,
/// routing them to their respective handlers while keeping the handlers decoupled from each other.
///
/// This pattern ensures that the components remain isolated from each other, promoting a cleaner and more maintainable code architecture.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Asynchronously dispatches a message to its respective handler for processing.
    /// </summary>
    /// <typeparam name="T">The type of the message.</typeparam>
    /// <param name="message">The message to be dispatched.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="NotSupportedException">There is no corrosponding handler for the message type <typeparamref name="T"/></exception>
    Task InvokeAsync<T>(T message, CancellationToken cancellationToken)
        where T : Message;

    /// <summary>
    /// Asynchronously dispatches a message to its respective handler for processing and returns a response message.
    /// </summary>
    /// <typeparam name="T">The type of the message.</typeparam>
    /// <typeparam name="TResult">The type of the result message.</typeparam>
    /// <param name="message">The message to be dispatched.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the result message.</returns>
    /// <exception cref="NotSupportedException">
    /// There is no corrosponding handler for the type combinations of <typeparamref name="T"/> and <typeparamref name="TResult"/>.
    /// </exception>
    Task<TResult> InvokeAsync<T, TResult>(T message, CancellationToken cancellationToken)
        where T : Message
        where TResult : Message;
}