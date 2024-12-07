namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Linq;
using static MooVC.Architecture.Ddd.Sequence_Resources;

public sealed record Sequence
    : IComparable<Sequence>
{
    private const ulong StartingNumber = 1;
    private const int SegmentSize = 8;
    private static readonly byte[] empty = new byte[SegmentSize];
    private readonly Lazy<Guid> signature;

    public Sequence()
        : this(Splice(Guid.NewGuid()), new byte[SegmentSize], StartingNumber)
    {
    }

    public Sequence(IReadOnlyList<byte> footer, IReadOnlyList<byte> header, ulong number)
    {
        Footer = footer.ToArray();
        Header = header.ToArray();
        Number = number;
        signature = Combine();
    }

    public static Sequence Empty { get; } = new Sequence(empty, empty, ulong.MinValue);

    public IReadOnlyList<byte> Footer { get; }

    public IReadOnlyList<byte> Header { get; }

    public bool IsEmpty => this == Empty;

    public bool IsNew => Number == StartingNumber;

    public ulong Number { get; }

    public Guid Signature => signature.Value;

    public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

    public static implicit operator ulong(Sequence? sequence)
    {
        return sequence?.Number ?? ulong.MinValue;
    }

    public static implicit operator Guid(Sequence? sequence)
    {
        return sequence?.Signature ?? Guid.Empty;
    }

    public static bool operator <(Sequence left, Sequence right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Sequence left, Sequence right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Sequence left, Sequence right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Sequence left, Sequence right)
    {
        return left.CompareTo(right) >= 0;
    }

    public int CompareTo(Sequence? other)
    {
        return other is null
            ? 1
            : Number.CompareTo(other.Number);
    }

    public bool IsNext(Sequence? previous)
    {
        return previous is { } && IsNext(previous.Footer, previous.Number);
    }

    public bool IsNext(IEnumerable<byte> footer, ulong number)
    {
        return !IsNew
            && (Number - number) == 1
            && Header.SequenceEqual(footer);
    }

    public Sequence Next()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException(NextSequenceIsEmpty);
        }

        checked
        {
            return new Sequence(Splice(Guid.NewGuid()), Footer, Number + 1);
        }
    }

    public Guid ToGuid()
    {
        if (IsEmpty)
        {
            return Guid.Empty;
        }

        var id = new List<byte>(Header);

        id.AddRange(Footer);

        return new(id.ToArray());
    }

    public override string ToString()
    {
        if (IsEmpty)
        {
            return Sequence_Resources.Empty;
        }

        return $"{Number} ({Signature:P})";
    }

    private static IReadOnlyList<byte> Splice(Guid signature)
    {
        return signature
            .ToByteArray()
            .Take(SegmentSize)
            .ToArray();
    }

    private Lazy<Guid> Combine()
    {
        return new Lazy<Guid>(() => new Guid(Header.Concat(Footer).ToArray()));
    }
}