namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Serialization;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class AggregateReferenceMismatchException<TAggregate>
    : ArgumentException
    where TAggregate : AggregateRoot
{
    public AggregateReferenceMismatchException(Reference reference)
        : base(FormatMessage(reference))
    {
        Reference = reference;
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private AggregateReferenceMismatchException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Reference = info.TryGetReference(nameof(Reference));
    }

    public Reference Reference { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddReference(nameof(Reference), Reference);
    }

    private static string FormatMessage(Reference reference)
    {
        _ = IsNotNull(reference, message: AggregateReferenceMismatchExceptionReferenceRequired);

        return Format(AggregateReferenceMismatchExceptionMessage, reference.Id, reference.Type.Name, typeof(TAggregate).Name);
    }
}