namespace MooVC.Architecture.Ddd.Services.Reconciliation
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