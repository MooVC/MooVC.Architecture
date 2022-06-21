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
    public AggregateInvariantsNotSatisfiedDomainException(
        Request request,
        Reference aggregate,
        IEnumerable<string> explainations)
        : base(
              request.Context,
              aggregate,
              Format(
                  AggregateInvariantsNotSatisfiedDomainExceptionMessage,
                  request.GetType().Name,
                  aggregate.Type.Name,
                  Join(NewLine, explainations)))
    {
        Explainations = explainations.Snapshot();
    }

    private AggregateInvariantsNotSatisfiedDomainException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Explainations = info.TryGetEnumerable<string>(nameof(Explainations));
    }

    public IEnumerable<string> Explainations { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddEnumerable(nameof(Explainations), Explainations);
    }
}