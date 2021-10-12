namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Collections.Generic;
    using MooVC.Serialization;

    [Serializable]
    public abstract class EnumerableResult<T>
        : Message
    {
        protected EnumerableResult(Message context, IEnumerable<T> results)
            : base(context)
        {
            Results = results.Snapshot();
        }

        protected EnumerableResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Results = info.TryGetEnumerable<T>(nameof(Results));
        }

        public IEnumerable<T> Results { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return Results.GetEnumerator();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddEnumerable(nameof(Results), Results);
        }
    }
}