﻿namespace MooVC.Architecture.Cqrs.Services;

using System.Threading;
using System.Threading.Tasks;

public abstract class SynchronousQueryEngine
    : IQueryEngine
{
    public virtual Task<TResult> QueryAsync<TResult>(CancellationToken? cancellationToken = default)
        where TResult : Message
    {
        return Task.FromResult(PerformQuery<TResult>());
    }

    public virtual Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken? cancellationToken = default)
        where TQuery : Message
        where TResult : Message
    {
        return Task.FromResult(PerformQuery<TQuery, TResult>(query));
    }

    protected abstract TResult PerformQuery<TResult>()
        where TResult : Message;

    protected abstract TResult PerformQuery<TQuery, TResult>(TQuery query)
        where TQuery : Message
        where TResult : Message;
}