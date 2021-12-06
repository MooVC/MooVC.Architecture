namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Runtime.Serialization;
    using MooVC.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class Reference
        : Value,
          IEquatable<Reference>
    {
        private protected Reference(Guid id, Type type, SignedVersion? version)
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

        public Type Type { get; }

        public SignedVersion Version { get; } = SignedVersion.Empty;

        public static implicit operator Guid(Reference? reference)
        {
            return reference?.Id ?? Guid.Empty;
        }

        [return: NotNullIfNotNull("reference")]
        public static implicit operator Type?(Reference? reference)
        {
            return reference?.Type;
        }

        public static implicit operator SignedVersion(Reference? reference)
        {
            return reference?.Version ?? SignedVersion.Empty;
        }

        public static bool operator ==(Reference? first, Reference? second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(Reference? first, Reference? second)
        {
            return NotEqualOperator(first, second);
        }

        public static Reference Create(Guid id, string typeName, SignedVersion? version = default)
        {
            var aggregate = Type.GetType(typeName, true);

            return Create(id, aggregate!, version: version);
        }

        public static Reference Create(Guid id, Type type, SignedVersion? version = default)
        {
            _ = ArgumentNotNull(
                type,
                nameof(type),
                ReferenceCreateTypeRequired);

            Type reference = typeof(Reference<>);
            Type aggregate = reference.MakeGenericType(type);

            if (id == Guid.Empty)
            {
                return (Reference)aggregate
                    .GetProperty(nameof(Empty), aggregate)!
                    .GetValue(default)!;
            }

            return (Reference)Activator.CreateInstance(
                aggregate,
                BindingFlags.NonPublic | BindingFlags.Instance,
                default,
                new object?[]
                {
                    id,
                    type,
                    version,
                },
                default)!;
        }

        public static Reference Create<TAggregate>(Guid id, SignedVersion? version = default)
            where TAggregate : AggregateRoot
        {
            return Create(id, typeof(TAggregate), version: version);
        }

        public static Reference Create(AggregateRoot aggregate)
        {
            _ = ArgumentNotNull(
                aggregate,
                nameof(aggregate),
                ReferenceCreateAggregateRequired);

            return Create(aggregate.Id, aggregate.GetType(), version: aggregate.Version);
        }

        public override bool Equals(object? other)
        {
            return EqualOperator(this, other as Reference);
        }

        public bool Equals(Reference? other)
        {
            return EqualOperator(this, other);
        }

        public override bool Equals(Value? other)
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

        public override string ToString()
        {
            if (IsEmpty)
            {
                return Type.FullName!;
            }

            return $"{Type.FullName} [{Id:P}, {Version}]";
        }

        protected virtual Guid DeserializeId(SerializationInfo info, StreamingContext context)
        {
            return info.TryGetValue(nameof(Id), defaultValue: Guid.Empty);
        }

        protected virtual Type DeserializeType(SerializationInfo info, StreamingContext context)
        {
            return DeserializeType(default, info, context);
        }

        protected virtual Type DeserializeType(
            Type? @default,
            SerializationInfo info,
            StreamingContext context)
        {
            string? typeName;

            typeName = info.TryGetInternalString(nameof(typeName));

            if (IsNullOrEmpty(typeName))
            {
                if (@default is null)
                {
                    throw new SerializationException(ReferenceDeserializeTypeTypeIndeterminate);
                }

                return @default;
            }

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