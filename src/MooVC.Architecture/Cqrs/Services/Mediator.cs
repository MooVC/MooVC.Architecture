namespace MooVC.Architecture.Cqrs.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using MooVC.Architecture.Services;
using static MooVC.Architecture.Cqrs.Services.Mediator_Resources;
using static System.String;

public sealed class Mediator
    : IMediator
{
    private readonly IServiceProvider provider;

    public Mediator(IServiceProvider provider)
    {
        this.provider = Guard.Against.Null(provider, message: ProviderRequired);
    }

    public Task InvokeAsync<T>(T message, CancellationToken cancellationToken)
        where T : Message
    {
        IHandler<T>? handler = provider.GetService<IHandler<T>>()
            ?? throw new NotSupportedException(Format(InvokeAsyncHandlerNotFound, typeof(T).FullName));

        return handler.ExecuteAsync(message, cancellationToken);
    }

    public Task<TResult> InvokeAsync<T, TResult>(T message, CancellationToken cancellationToken)
        where T : Message
        where TResult : Message
    {
        IHandler<T, TResult>? handler = provider.GetService<IHandler<T, TResult>>()
            ?? throw new NotSupportedException(Format(InvokeAsyncHandlerCombinationNotFound, typeof(T).FullName, typeof(TResult).FullName));

        return handler.ExecuteAsync(message, cancellationToken);
    }
}