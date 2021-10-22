namespace MooVC.Architecture.Ddd.Services
{
    public static class Ensure
    {
        public static void AggregateDoesNotConflict<TAggregate>(
            TAggregate proposed,
            SignedVersion? currentVersion = default)
            where TAggregate : AggregateRoot
        {
            if (currentVersion is { } && !currentVersion.IsEmpty)
            {
                if (!proposed.Version.IsNext(currentVersion))
                {
                    throw new AggregateConflictDetectedException<TAggregate>(proposed.Id, currentVersion, proposed.Version);
                }
            }
            else if (!proposed.Version.IsNew)
            {
                throw new AggregateConflictDetectedException<TAggregate>(proposed.Id, proposed.Version);
            }
        }

        public static void AggregateDoesNotConflict<TAggregate>(
            TAggregate proposed,
            TAggregate? current)
            where TAggregate : AggregateRoot
        {
            AggregateDoesNotConflict(proposed, currentVersion: current?.Version);
        }
    }
}