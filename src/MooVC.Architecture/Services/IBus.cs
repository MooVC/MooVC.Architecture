namespace MooVC.Architecture.Services
{
    public interface IBus
    {
        void Invoke<T>(T message)
            where T : Message;
    }
}