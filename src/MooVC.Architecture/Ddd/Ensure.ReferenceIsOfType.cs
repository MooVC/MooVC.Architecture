namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsOfType<TAggregate>(
            [NotNull] Reference? reference,
            string argumentName)
            where TAggregate : AggregateRoot
        {
            ReferenceIsOfType<TAggregate>(
                reference,
                argumentName,
                Format(EnsureReferenceIsOfTypeMessage, reference?.Type.Name, typeof(TAggregate).Name));
        }

        public static void ReferenceIsOfType<TAggregate>(
            [NotNull] Reference? reference,
            string argumentName,
            string message)
            where TAggregate : AggregateRoot
        {
            if (reference is null || !typeof(TAggregate).IsAssignableFrom(reference.Type))
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}