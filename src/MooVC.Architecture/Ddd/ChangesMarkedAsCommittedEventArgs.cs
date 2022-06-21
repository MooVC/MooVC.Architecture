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

    private ChangesMarkedAsCommittedEventArgs(SerializationInfo info, StreamingContext context)
    {
        Changes = info.TryGetEnumerable<DomainEvent>(nameof(Changes));
    }

    public IEnumerable<DomainEvent> Changes { get; }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddEnumerable(nameof(Changes), Changes);
    }
}