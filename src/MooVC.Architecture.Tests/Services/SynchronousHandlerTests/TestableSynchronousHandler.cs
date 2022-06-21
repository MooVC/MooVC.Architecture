namespace MooVC.Architecture.Services.SynchronousHandlerTests;

using System;

public sealed class TestableSynchronousHandler<TMessage>
    : SynchronousHandler<TMessage>
    where TMessage : Message
{
    private readonly Action<TMessage>? execute;

    public TestableSynchronousHandler(Action<TMessage>? execute = default)
    {
        this.execute = execute;
    }

    protected override void PerformExecute(TMessage message)
    {
        if (execute is null)
        {
            throw new NotImplementedException();
        }

        execute(message);
    }
}