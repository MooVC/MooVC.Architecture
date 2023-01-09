namespace MooVC.Architecture.Services;

using System;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Serialization;
using static MooVC.Architecture.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class MessageAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    protected MessageAsyncEventArgs(Message message, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Message = IsNotNull(message, message: MessageEventArgsMessageRequired);
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    protected MessageAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Message = info.GetValue<Message>(nameof(Message));
    }

    public Message Message { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Message), Message);
    }
}