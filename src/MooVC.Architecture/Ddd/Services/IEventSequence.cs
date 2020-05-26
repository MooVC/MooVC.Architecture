namespace MooVC.Architecture.Ddd.Services
{
    using System;

    public interface IEventSequence
    {
        public ulong Sequence { get; }

        public DateTime LastUpdated { get; }
    }
}