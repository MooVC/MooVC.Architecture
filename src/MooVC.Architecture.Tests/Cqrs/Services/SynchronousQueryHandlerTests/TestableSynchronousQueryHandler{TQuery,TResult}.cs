namespace MooVC.Architecture.Cqrs.Services.SynchronousQueryHandlerTests;

using System;

public sealed class TestableSynchronousQueryHandler<TQuery, TResult>
    : SynchronousQueryHandler<TQuery, TResult>
    where TQuery : Message
    where TResult : Message
{
    private readonly Func<TQuery, TResult>? execute;

    public TestableSynchronousQueryHandler(Func<TQuery, TResult>? execute = default)
    {
        this.execute = execute;
    }

    protected override TResult PerformExecute(TQuery query)
    {
        if (execute is null)
        {
            throw new NotImplementedException();
        }

        return execute(query);
    }
}