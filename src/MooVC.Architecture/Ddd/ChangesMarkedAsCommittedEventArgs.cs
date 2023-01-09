namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Collections.Generic;
using MooVC.Serialization;

[Serializable]
public sealed class ChangesMarkedAsCommittedEventArgs
    : EventArgs,
      ISerializable
{
    internal ChangesMarkedAsCommittedEventArgs(IEnumerable<DomainEvent> changes)
    {
        Changes = changes.Snapshot();
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private ChangesMarkedAsCommittedEventArgs(SerializationInfo info, StreamingContext context)
    {
        Changes = info.TryGetEnumerable<DomainEvent>(nameof(Changes));
    }

    public IEnumerable<DomainEvent> Changes { get; }

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
        _ = info.TryAddEnumerable(nameof(Changes), Changes);
    }
}