namespace MooVC.Architecture.Cqrs.Services.SynchronousQueryEngineTests;

using System;

public sealed class TestableSynchronousQueryEngine
    : SynchronousQueryEngine
{
    private readonly Func<object>? parameterless;
    private readonly Func<object, object>? parameters;

    public TestableSynchronousQueryEngine(
        Func<object>? parameterless = default,
        Func<object, object>? parameters = default)
    {
        this.parameterless = parameterless;
        this.parameters = parameters;
    }

    protected override TResult PerformQuery<TResult>()
    {
        if (parameterless is null)
        {
            throw new NotImplementedException();
        }

        return (TResult)parameterless();
    }

    protected override TResult PerformQuery<TQuery, TResult>(TQuery query)
    {
        if (parameters is null)
        {
            throw new NotImplementedException();
        }

        return (TResult)parameters(query);
    }
}