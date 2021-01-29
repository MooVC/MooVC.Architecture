namespace MooVC.Architecture.Ddd
{
    using System;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsNotEmpty(Reference reference, string argumentName)
        {
            ReferenceIsNotEmpty(
                reference,
                argumentName,
                Format(EnsureReferenceIsNotEmptyMessage, reference?.Type.Name));
        }

        public static void ReferenceIsNotEmpty(Reference reference, string argumentName, string message)
        {
            if (reference is null || reference.IsEmpty)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}