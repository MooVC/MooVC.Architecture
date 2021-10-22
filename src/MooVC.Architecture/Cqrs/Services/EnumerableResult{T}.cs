namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;
    using MooVC.Collections.Generic;

    [Serializable]
    public abstract class EnumerableResult<T>
        : Result<IEnumerable<T>>
    {
        protected EnumerableResult(Message context, IEnumerable<T> values)
            : base(context, values.Snapshot())
        {
        }

        protected EnumerableResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ulong Count => (ulong)Value.LongCount();

        public T this[int index] => Value.ElementAt(index);

        [return: NotNullIfNotNull("result")]
        public static implicit operator T[](EnumerableResult<T>? result)
        {
            if (result is null)
            {
                return Array.Empty<T>();
            }

            return result.Value.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }
}