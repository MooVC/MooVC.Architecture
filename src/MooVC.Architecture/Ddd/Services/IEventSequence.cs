namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;

    public interface IEventSequence
        : ISerializable
    {
        public ulong Sequence { get; }

        public DateTime TimeStamp { get; }
    }
}