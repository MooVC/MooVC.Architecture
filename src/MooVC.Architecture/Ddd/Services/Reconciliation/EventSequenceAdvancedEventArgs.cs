namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public sealed class EventSequenceAdvancedEventArgs
        : EventArgs,
          ISerializable
    {
        internal EventSequenceAdvancedEventArgs(ulong sequence)
        {
            Sequence = sequence;
        }

        private EventSequenceAdvancedEventArgs(SerializationInfo info, StreamingContext context)
        {
            Sequence = info.GetUInt64(nameof(Sequence));
        }

        public ulong Sequence { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Sequence), Sequence);
        }
    }
}