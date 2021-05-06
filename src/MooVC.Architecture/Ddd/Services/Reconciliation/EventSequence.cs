namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    [Serializable]
    public sealed class EventSequence
        : IEventSequence,
          ISerializable
    {
        public EventSequence(ulong sequence, DateTimeOffset? timeStamp = default)
        {
            Sequence = sequence;
            TimeStamp = timeStamp.GetValueOrDefault(DateTimeOffset.UtcNow);
        }

        private EventSequence(SerializationInfo info, StreamingContext context)
        {
            Sequence = info.GetUInt64(nameof(Sequence));
            TimeStamp = info.GetValue<DateTimeOffset>(nameof(TimeStamp));
        }

        public ulong Sequence { get; }

        public DateTimeOffset TimeStamp { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Sequence), Sequence);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}