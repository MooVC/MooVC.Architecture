namespace MooVC.Architecture.Services
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class HandlerExecutionFailureException<TMessage>
        : InvalidOperationException
        where TMessage : Message
    {
        public HandlerExecutionFailureException(TMessage context, Type handler, Exception cause)
            : base(
                  string.Format(
                      HandlerFailureExceptionMessage,
                      handler.Name,
                      context.Id,
                      context.CorrelationId,
                      cause.Message),
                  cause)
        {
            Context = context;
            Handler = handler;
        }

        public TMessage Context { get; }

        public Type Handler { get; }
    }
}