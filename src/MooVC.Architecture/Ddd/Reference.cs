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
            : this(aggregate.Id, aggregate.GetType())
        {
        }

        internal Reference(Guid id, Type type)
        {
            Id = id;
            Type = type;
        }

        private protected Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = DeserializeId(info, context);
            Type = DeserializeType(info, context);
        }

        public Guid Id { get; }

        public bool IsEmpty => Id == Guid.Empty;

        public Type Type { get; }

        public static bool operator ==(Reference? first, Reference? second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(Reference? first, Reference? second)
        {
            return NotEqualOperator(first, second);
        }

        public override bool Equals(object? other)
        {
            return EqualOperator(this, other as Reference);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            SerializeId(info, context);
            SerializeType(info, context);
        }

        public virtual bool IsMatch(AggregateRoot aggregate)
        {
            return Type == aggregate?.GetType() && Id == aggregate.Id;
        }

        protected virtual Guid DeserializeId(SerializationInfo info, StreamingContext context)
        {
            return info.TryGetValue<Guid>(nameof(Id));
        }

        protected virtual Type DeserializeType(SerializationInfo info, StreamingContext context)
        {
            string typeName;

            typeName = info.GetInternalString(nameof(typeName));

            return Type.GetType(typeName, true);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Type;
        }

        protected virtual void SerializeId(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddValue(nameof(Id), Id);
        }

        protected virtual void SerializeType(SerializationInfo info, StreamingContext context)
        {
            string typeName = Type.AssemblyQualifiedName;

            info.AddInternalValue(nameof(typeName), typeName);
        }

        private static bool EqualOperator(Reference? left, Reference? right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            if (left is null)
            {
                return true;
            }

            return left.Id == right!.Id && left.Type == right.Type;
        }

        private static bool NotEqualOperator(Reference? left, Reference? right)
        {
            return !EqualOperator(left, right);
        }
    }
}