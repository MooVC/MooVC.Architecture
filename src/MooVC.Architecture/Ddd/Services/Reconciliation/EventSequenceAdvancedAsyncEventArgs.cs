namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Runtime.Serialization;
using System.Threading;

[Serializable]
public sealed class EventSequenceAdvancedAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    internal EventSequenceAdvancedAsyncEventArgs(
        ulong sequence,
        CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Sequence = sequence;
    }

    private EventSequenceAdvancedAsyncEventArgs(
        SerializationInfo info,
        StreamingContext context)
    {
        Sequence = info.GetUInt64(nameof(Sequence));
    }

    public ulong Sequence { get; }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Sequence), Sequence);
    }
}