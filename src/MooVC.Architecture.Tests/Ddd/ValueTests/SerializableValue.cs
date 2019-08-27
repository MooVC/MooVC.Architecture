namespace MooVC.Architecture.Ddd.ValueTests
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    internal sealed class SerializableValue
        : Value
    {
        public SerializableValue(int first = 0, string second = null, Value third = null)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public SerializableValue(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            First = info.GetInt32(nameof(First));
            Second = info.GetString(nameof(Second));
            Third = (Value)info.GetValue(nameof(Third), typeof(Value));
        }

        public int First { get; }

        public string Second { get; }

        public Value Third { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(First), First);
            info.AddValue(nameof(Second), Second);
            info.AddValue(nameof(Third), Third);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return First;
            yield return Second;
            yield return Third;
        }
    }
}