namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public sealed class EventSequence
        : IEventSequence,
          ISerializable
    {
        public EventSequence(ulong sequence, DateTime? lastUpdated = default)
        {
            LastUpdated = lastUpdated.GetValueOrDefault(DateTime.UtcNow);
            Sequence = sequence;
        }

        private EventSequence(SerializationInfo info, StreamingContext context)
        {
            LastUpdated = info.GetDateTime(nameof(LastUpdated));
            Sequence = info.GetUInt64(nameof(Sequence));
        }

        public DateTime LastUpdated { get; }

        public ulong Sequence { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(LastUpdated), LastUpdated);
            info.AddValue(nameof(Sequence), Sequence);
        }
    }
}