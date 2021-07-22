namespace MooVC.Architecture.Ddd.Collections
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using MooVC.Collections.Generic;

    public sealed class UnversionedReferenceDictionary<TAggregate, T>
        : IDictionary<Reference<TAggregate>, T>
        where TAggregate : AggregateRoot
    {
        private readonly IDictionary<Reference<TAggregate>, T> @internal = new ConcurrentDictionary<Reference<TAggregate>, T>();

        public UnversionedReferenceDictionary(IDictionary<Reference<TAggregate>, T>? existing = default)
        {
            if (existing is { })
            {
                existing.ForEach(Add);
            }
        }

        public ICollection<Reference<TAggregate>> Keys => @internal.Keys;

        public ICollection<T> Values => @internal.Values;

        public int Count => @internal.Count;

        public bool IsReadOnly => @internal.IsReadOnly;

        public T this[Reference<TAggregate> key]
        {
            get => @internal[key.ToUnversioned()];
            set => @internal[key.ToUnversioned()] = value;
        }

        public void Add(Reference<TAggregate> key, T value)
        {
            @internal.Add(key.ToUnversioned(), value);
        }

        public void Add(KeyValuePair<Reference<TAggregate>, T> item)
        {
            @internal.Add(item.ToUnversioned());
        }

        public void Clear()
        {
            @internal.Clear();
        }

        public bool Contains(KeyValuePair<Reference<TAggregate>, T> item)
        {
            return @internal.Contains(item.ToUnversioned());
        }

        public bool ContainsKey(Reference<TAggregate> key)
        {
            return @internal.ContainsKey(key.ToUnversioned());
        }

        public void CopyTo(KeyValuePair<Reference<TAggregate>, T>[] array, int arrayIndex)
        {
            @internal.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Reference<TAggregate>, T>> GetEnumerator()
        {
            return @internal.GetEnumerator();
        }

        public bool Remove(Reference<TAggregate> key)
        {
            return @internal.Remove(key.ToUnversioned());
        }

        public bool Remove(KeyValuePair<Reference<TAggregate>, T> item)
        {
            return @internal.Remove(item.ToUnversioned());
        }

        public bool TryGetValue(Reference<TAggregate> key, [MaybeNullWhen(false)] out T value)
        {
            return @internal.TryGetValue(key.ToUnversioned(), out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)@internal).GetEnumerator();
        }
    }
}