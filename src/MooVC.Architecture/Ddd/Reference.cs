namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    [Serializable]
    public class Reference
        : Value
    {
        internal Reference(AggregateRoot aggregate)
            : this(aggregate.Id, aggregate.GetType(), version: aggregate.Version)
        {
        }

        internal Reference(Guid id, Type type, SignedVersion? version = default)
        {
            Id = id;
            Type = type;
            Version = version ?? SignedVersion.Empty;
        }

        private protected Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = DeserializeId(info, context);
            Type = DeserializeType(info, context);
            Version = DeserializeVersion(info, context);
        }

        public Guid Id { get; }

        public bool IsEmpty => Id == Guid.Empty;

        public bool IsVersioned => !Version.IsEmpty;

        public virtual Type Type { get; }

        public SignedVersion Version { get; } = SignedVersion.Empty;

        public static bool operator ==(Reference? first, Reference? second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(Reference? first, Reference? second)
        {
            return NotEqualOperator(first, second);
        }

        public static Reference Create(string typeName, Guid id, SignedVersion? version = default)
        {
            var aggregate = Type.GetType(typeName, true);
            Type reference = typeof(Reference<>);
            Type typed = reference.MakeGenericType(aggregate!);

            return (Reference)Activator.CreateInstance(typed, id, version)!;
        }

        public override bool Equals(object? other)
        {
            return EqualOperator(this, other as Reference);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            SerializeId(info, context);
            SerializeType(info, context);
            SerializeVersion(info, context);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool IsMatch(AggregateRoot aggregate)
        {
            if (aggregate is null)
            {
                return false;
            }

            Type type = aggregate.GetType();

            return Id == aggregate.Id
                && (Type.IsAssignableFrom(type) || type.IsAssignableFrom(Type))
                && (Version.IsEmpty || Version == aggregate.Version);
        }

        protected virtual Guid DeserializeId(SerializationInfo info, StreamingContext context)
        {
            return info.TryGetValue(nameof(Id), defaultValue: Guid.Empty);
        }

        protected virtual Type DeserializeType(SerializationInfo info, StreamingContext context)
        {
            string? typeName;

            typeName = info.TryGetInternalString(nameof(typeName));

            var type = Type.GetType(typeName, true);

            return type!;
        }

        protected virtual SignedVersion DeserializeVersion(SerializationInfo info, StreamingContext context)
        {
            return info.TryGetValue(nameof(Version), defaultValue: SignedVersion.Empty);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Type;
            yield return Version;
        }

        protected virtual void SerializeId(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddValue(nameof(Id), Id, defaultValue: Guid.Empty);
        }

        protected virtual void SerializeType(SerializationInfo info, StreamingContext context)
        {
            string? typeName = Type.AssemblyQualifiedName;

            _ = info.TryAddInternalValue(nameof(typeName), typeName);
        }

        protected virtual void SerializeVersion(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddValue(nameof(Version), Version, defaultValue: SignedVersion.Empty);
        }

        private static bool EqualOperator(Reference? left, Reference? right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            if (left is null)
            {
                return Equals(left, right);
            }

            return left.Id == right!.Id
                && (left.Type.IsAssignableFrom(right.Type) || right.Type.IsAssignableFrom(left.Type))
                && (left.Version == SignedVersion.Empty || right.Version == SignedVersion.Empty || left.Version == right.Version);
        }

        private static bool NotEqualOperator(Reference? left, Reference? right)
        {
            return !EqualOperator(left, right);
        }
    }
}