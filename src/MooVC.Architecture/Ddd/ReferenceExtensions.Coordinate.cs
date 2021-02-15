namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    public static partial class ReferenceExtensions
    {
        public static void Coordinate(this Reference reference, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(reference, nameof(reference), ReferenceExtensionsCoordinateReferenceeRequired);

            reference
                .Type
                .Coordinate(
                    reference.Id,
                    operation,
                    timeout: timeout);
        }

        public static async Task CoordinateAsync(this Reference reference, Func<Task> operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(reference, nameof(reference), ReferenceExtensionsCoordinateReferenceeRequired);

            await reference
                .Type
                .CoordinateAsync(
                    reference.Id,
                    operation,
                    timeout: timeout);
        }
    }
}