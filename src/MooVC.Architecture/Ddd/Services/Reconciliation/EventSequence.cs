namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class EventSequence
        : IEventSequence,
          ISerializable
    {
        public EventSequence(ulong sequence, DateTime? timeStamp = default)
        {
            Sequence = sequence;
            TimeStamp = timeStamp.GetValueOrDefault(DateTime.UtcNow);
        }

        private EventSequence(SerializationInfo info, StreamingContext context)
        {
            Sequence = info.GetUInt64(nameof(Sequence));
            TimeStamp = info.GetDateTime(nameof(TimeStamp));
        }

        public ulong Sequence { get; }

        public DateTime TimeStamp { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Sequence), Sequence);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}