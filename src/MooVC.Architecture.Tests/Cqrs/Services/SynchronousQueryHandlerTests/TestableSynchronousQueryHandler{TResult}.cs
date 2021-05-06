namespace MooVC.Architecture.Cqrs.Services.SynchronousQueryHandlerTests
{
    using System;

    public sealed class TestableSynchronousQueryHandler<TResult>
        : SynchronousQueryHandler<TResult>
        where TResult : Message
    {
        private readonly Func<TResult>? execute;

        public TestableSynchronousQueryHandler(Func<TResult>? execute = default)
        {
            this.execute = execute;
        }

        protected override TResult PerformExecute()
        {
            if (execute is null)
            {
                throw new NotImplementedException();
            }

            return execute();
        }
    }
}