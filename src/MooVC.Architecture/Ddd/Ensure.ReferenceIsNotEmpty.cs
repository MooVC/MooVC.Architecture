namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsNotEmpty(
            [NotNull] Reference? reference,
            string argumentName)
        {
            ReferenceIsNotEmpty(
                reference,
                argumentName,
                Format(EnsureReferenceIsNotEmptyMessage, reference?.Type.Name));
        }

        public static void ReferenceIsNotEmpty(
            [NotNull] Reference? reference,
            string argumentName,
            string message)
        {
            if (reference is null || reference.IsEmpty)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}