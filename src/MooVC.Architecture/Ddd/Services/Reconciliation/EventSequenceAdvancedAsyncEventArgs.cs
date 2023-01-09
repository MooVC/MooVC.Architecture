namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Runtime.Serialization;
using System.Threading;

[Serializable]
public sealed class EventSequenceAdvancedAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    internal EventSequenceAdvancedAsyncEventArgs(ulong sequence, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Sequence = sequence;
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private EventSequenceAdvancedAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Sequence = info.GetUInt64(nameof(Sequence));
    }

    public ulong Sequence { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Sequence), Sequence);
    }
}