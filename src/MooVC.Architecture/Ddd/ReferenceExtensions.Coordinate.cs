namespace MooVC.Architecture.Ddd
{
    using System;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    public static partial class ReferenceExtensions
    {
        public static void Coordinate(this Reference reference, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(reference, nameof(reference), ReferenceExtensionsCoordinateReferenceeRequired);

            reference.Type.Coordinate(
                reference.Id,
                operation,
                timeout: timeout);
        }
    }
}