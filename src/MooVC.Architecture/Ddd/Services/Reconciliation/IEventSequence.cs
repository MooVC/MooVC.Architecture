namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;

    public interface IEventSequence
    {
        public ulong Sequence { get; }

        public DateTime TimeStamp { get; }
    }
}