namespace MooVC.Architecture.Ddd.Collections;

using System.Collections.Generic;

internal static partial class KeyValuePairExtensions
{
    public static KeyValuePair<Reference<TAggregate>, T> ToUnversioned<TAggregate, T>(this KeyValuePair<Reference<TAggregate>, T> item)
        where TAggregate : AggregateRoot
    {
        if (item.Key.IsVersioned)
        {
            return new KeyValuePair<Reference<TAggregate>, T>(item.Key.ToUnversioned(), item.Value);
        }

        return item;
    }
}