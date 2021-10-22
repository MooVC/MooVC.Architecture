namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    [Serializable]
    public abstract class Result<T>
        : Message
        where T : notnull
    {
        protected Result(Message context, T value)
            : base(context)
        {
            Value = value;
        }

        protected Result(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Value = info.GetValue<T>(nameof(Value));
        }

        public T Value { get; }

        [return: NotNullIfNotNull("result")]
        public static implicit operator T?(Result<T>? result)
        {
            if (result is null)
            {
                return default;
            }

            return result.Value;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Value), Value);
        }
    }
}