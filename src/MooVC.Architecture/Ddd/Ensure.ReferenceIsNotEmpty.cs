namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    public static partial class Ensure
    {
        public static void ReferenceIsNotEmpty(IReference reference, string argumentName)
        {
            ReferenceIsNotEmpty(
                reference, 
                argumentName, 
                string.Format(EnsureReferenceIsNotEmptyMessage, reference?.Type.Name));
        }

        public static void ReferenceIsNotEmpty(IReference reference, string argumentName, string message)
        {
            if (reference == null || reference.IsEmpty)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}