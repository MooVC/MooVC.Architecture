namespace MooVC.Architecture.Cqrs.Services;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using MooVC.Collections.Generic;

[Serializable]
public abstract class EnumerableResult<TQuery, T>
    : Result<TQuery, IEnumerable<T>>
    where TQuery : Message
{
    private readonly Lazy<ulong> count;

    protected EnumerableResult(TQuery query, IEnumerable<T> values)
        : base(query, values.Snapshot())
    {
        count = new(CalculateCount);
    }

    protected EnumerableResult(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        count = new(CalculateCount);
    }

    public ulong Count => count.Value;

    public bool HasResults => Count > 0;

    public T this[int index] => Value.ElementAt(index);

    [return: NotNullIfNotNull("result")]
    public static implicit operator T[](EnumerableResult<TQuery, T>? result)
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

    private ulong CalculateCount()
    {
        return (ulong)Value.LongCount();
    }
}