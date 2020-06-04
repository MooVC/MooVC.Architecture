namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public sealed class EventSequence
        : IEventSequence
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

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Sequence), Sequence);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}