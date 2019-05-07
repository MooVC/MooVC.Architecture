namespace MooVC.Architecture.Services
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class Handler<TMessage>
        : IHandler<TMessage>
        where TMessage : Message
    {
        public void Execute(TMessage message)
        {
            ProcessExecute<TMessage>(message, ProcessMessage);
        }

        protected void ProcessExecute<TContextualMessage>(TContextualMessage message, Action<TContextualMessage> process)
            where TContextualMessage : Message
        {
            try
            {
                EnsureMessageIsValid(message);

                process(message);
            }
            catch (Exception ex)
            {
                throw new HandlerExecutionFailureException<TContextualMessage>(message, GetType(), ex);
            }
        }

        protected abstract void ProcessMessage(TMessage message);

        private void EnsureMessageIsValid<TContextualMessage>(TContextualMessage message)
        {
            Validator.ValidateObject(message, new ValidationContext(message), true);
        }
    }
}