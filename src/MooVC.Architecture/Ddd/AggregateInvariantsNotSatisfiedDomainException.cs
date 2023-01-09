namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Collections.Generic;
using MooVC.Serialization;
using static System.Environment;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;

[Serializable]
public sealed class AggregateInvariantsNotSatisfiedDomainException
    : DomainException
{
    public AggregateInvariantsNotSatisfiedDomainException(Request request, Reference aggregate, IEnumerable<string> explainations)
        : base(aggregate, request, FormatMessage(aggregate, explainations, request))
    {
        Explainations = explainations.Snapshot();
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private AggregateInvariantsNotSatisfiedDomainException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Explainations = info.TryGetEnumerable<string>(nameof(Explainations));
    }

    public IEnumerable<string> Explainations { get; }

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

        _ = info.TryAddEnumerable(nameof(Explainations), Explainations);
    }

    private static string FormatMessage(Reference aggregate, IEnumerable<string> explainations, Request request)
    {
        return Format(
            AggregateInvariantsNotSatisfiedDomainExceptionMessage,
            request.GetType().Name,
            aggregate.Type.Name,
            Join(NewLine, explainations));
    }
}