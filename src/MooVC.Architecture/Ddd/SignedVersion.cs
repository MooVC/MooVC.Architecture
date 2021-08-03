namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using MooVC.Collections.Generic;
    using MooVC.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class SignedVersion
        : Value,
          IComparable<SignedVersion>
    {
        private const ulong DefaultNumber = 1;
        private const int SplicePortion = 8;

        private static readonly byte[] emptySegment = new byte[SplicePortion];

        private static readonly Lazy<SignedVersion> empty = new(
            () => new SignedVersion(emptySegment, emptySegment, 0));

        private readonly Lazy<Guid> signature;

        internal SignedVersion()
            : this(Splice(Guid.NewGuid()), new byte[SplicePortion], DefaultNumber)
        {
        }

        internal SignedVersion(SignedVersion previous)
        {
            ArgumentNotNull(previous, nameof(previous), SignedVersionPreviousRequired);

            ArgumentIsAcceptable(
                previous,
                nameof(previous),
                _ => !previous.Footer.SequenceEqual(emptySegment),
                Format(SignedVersionPreviousFooterInvalid, previous.Number));

            Footer = Splice(Guid.NewGuid());
            Header = previous.Footer;
            Number = previous.Number + 1;
            signature = Combine();
        }

        private SignedVersion(IEnumerable<byte> footer, IEnumerable<byte> header, ulong number)
        {
            Footer = footer.Snapshot();
            Header = header.Snapshot();
            Number = number;
            signature = Combine();
        }

        private SignedVersion(SerializationInfo info, StreamingContext context)
               : base(info, context)
        {
            Footer = info.TryGetEnumerable(nameof(Footer), emptySegment);
            Header = info.TryGetEnumerable(nameof(Header), emptySegment);
            Number = info.TryGetValue<ulong>(nameof(Number));
            signature = Combine();
            TimeStamp = info.TryGetValue(nameof(TimeStamp), defaultValue: DateTimeOffset.MinValue);
        }

        public static SignedVersion Empty => empty.Value;

        public IEnumerable<byte> Footer { get; }

        public IEnumerable<byte> Header { get; }

        public bool IsEmpty => this == Empty;

        public bool IsNew => Number == DefaultNumber;

        public ulong Number { get; }

        public Guid Signature => signature.Value;

        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

        public int CompareTo(SignedVersion? other)
        {
            return other is { }
                ? Number.CompareTo(other.Number)
                : 1;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddEnumerable(nameof(Footer), Footer, predicate: IsNotEmptySegment);
            _ = info.TryAddEnumerable(nameof(Header), Header, predicate: IsNotEmptySegment);
            _ = info.TryAddValue(nameof(Number), Number);
            _ = info.TryAddValue(nameof(TimeStamp), TimeStamp, defaultValue: DateTimeOffset.MinValue);
        }

        public bool IsNext(SignedVersion? previous)
        {
            return previous is { }
                && !IsNew
                && (Number - previous.Number) == 1
                && Header.SequenceEqual(previous.Footer);
        }

        public SignedVersion Next()
        {
            return new SignedVersion(this);
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
            return $"{Number} ({Signature})";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Footer;
            yield return Header;
            yield return Number;
        }

        private static bool IsNotEmptySegment(IEnumerable<byte> value)
        {
            return !value.SequenceEqual(emptySegment);
        }

        private static IEnumerable<byte> Splice(Guid signature)
        {
            return signature
                .ToByteArray()
                .Take(SplicePortion)
                .ToArray();
        }

        private Lazy<Guid> Combine()
        {
            return new Lazy<Guid>(() => new Guid(Enumerable.Concat(Header, Footer).ToArray()));
        }
    }
}