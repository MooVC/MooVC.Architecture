namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;
    using MooVC.Serialization;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public sealed class SignedVersion
        : Value,
          IComparable<SignedVersion>
    {
        private const ulong DefaultNumber = 1;
        private const int SplicePortion = 8;

        private static readonly byte[] emptySegment = new byte[SplicePortion];
        private static readonly Lazy<SignedVersion> empty = new Lazy<SignedVersion>(
            () => new SignedVersion(emptySegment, emptySegment, 0));

        private readonly Lazy<Guid> signature;

        internal SignedVersion()
            : this(Splice(Guid.NewGuid()), new byte[SplicePortion], DefaultNumber)
        {
        }

        internal SignedVersion(SignedVersion previous)
        {
            ArgumentNotNull(previous, nameof(previous), SignedVersionPreviousRequired);

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
            Footer = info.TryGetValue(nameof(Footer), defaultValue: emptySegment);
            Header = info.TryGetValue(nameof(Header), defaultValue: emptySegment);
            Number = info.TryGetValue<ulong>(nameof(Number));
            signature = Combine();
        }

        public static SignedVersion Empty => empty.Value;

        public IEnumerable<byte> Footer { get; }

        public IEnumerable<byte> Header { get; }

        public bool IsEmpty => this == Empty;

        public bool IsNew => Number == DefaultNumber;

        public ulong Number { get; }

        public Guid Signature => signature.Value;

        public int CompareTo(SignedVersion other)
        {
            return other is { }
                ? Number.CompareTo(other.Number)
                : 1;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddValue(nameof(Footer), Footer, defaultValue: emptySegment);
            _ = info.TryAddValue(nameof(Header), Header, defaultValue: emptySegment);
            _ = info.TryAddValue(nameof(Number), Number);
        }

        public bool IsNext(SignedVersion previous)
        {
            return previous is { }
                ? !IsNew && (Number - previous.Number) == 1 && Header.SequenceEqual(previous.Footer)
                : false;
        }

        public SignedVersion Next()
        {
            return new SignedVersion(this);
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Footer;
            yield return Header;
            yield return Number;
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
            return new Lazy<Guid>(() => new Guid(Header.Union(Footer).ToArray()));
        }
    }
}
