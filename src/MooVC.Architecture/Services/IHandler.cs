namespace MooVC.Architecture.Services
{
    public interface IHandler<TMessage>
        where TMessage : Message
    {
        void Execute(TMessage message);
    }
}