namespace MooVC.Architecture.MessageTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializableMessage
        : Message
    {
        public SerializableMessage()
        {
        }

        public SerializableMessage(Message context)
            : base(context)
        {
        }

        private SerializableMessage(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}