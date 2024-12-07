namespace MooVC.Architecture.Cqrs.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using MooVC.Architecture.Services;
using static System.String;
using static MooVC.Architecture.Cqrs.Services.Mediator_Resources;

/// <inheritdoc />
public sealed class Mediator
    : IMediator
{
    private readonly IServiceProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mediator"/> class, which serves as a mediator for handling requests of type <see cref="Message"/>.
    /// The <see cref="Mediator"/> mediates between the request and the corresponding handler by using the <paramref name="provider"/> to identify
    /// the configured instance of <see cref="IHandler{T}"/> or <see cref="IHandler{T, TResult}"/>.
    /// </summary>
    /// <remarks>
    /// The mediator pattern is used here to reduce direct dependencies between components, allowing for loose coupling and easier maintenance.
    /// </remarks>
    /// <param name="provider">
    /// The <see cref="IServiceProvider"/> used by the mediator to resolve service instances, particularly handlers for specific requests.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="provider"/> is <c>null</c>.</exception>
    public Mediator(IServiceProvider provider)
    {
        this.provider = Guard.Against.Null(provider, message: ProviderRequired);
    }

    /// <inheritdoc />
    public Task InvokeAsync<T>(T message, CancellationToken cancellationToken)
        where T : Message
    {
        IHandler<T>? handler = provider.GetService<IHandler<T>>()
            ?? throw new NotSupportedException(Format(InvokeAsyncHandlerNotFound, typeof(T).FullName));

        return handler.ExecuteAsync(message, cancellationToken);
    }

    /// <inheritdoc />
    public Task<TResult> InvokeAsync<T, TResult>(T message, CancellationToken cancellationToken)
        where T : Message
        where TResult : Message
    {
        IHandler<T, TResult>? handler = provider.GetService<IHandler<T, TResult>>()
            ?? throw new NotSupportedException(Format(InvokeAsyncHandlerCombinationNotFound, typeof(T).FullName, typeof(TResult).FullName));

        return handler.ExecuteAsync(message, cancellationToken);
    }
}