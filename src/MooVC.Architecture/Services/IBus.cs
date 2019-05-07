namespace MooVC.Architecture.Services
{
    using System;

    public interface IBus
    {
        void Invoke<T>(T message)
            where T : Message;
    }
}