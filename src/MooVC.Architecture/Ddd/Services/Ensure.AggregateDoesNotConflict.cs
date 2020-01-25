namespace MooVC.Architecture.Ddd.Services
{
    public static class Ensure
    {
        public static void AggregateDoesNotConflict<TAggregate>(AggregateRoot proposed, SignedVersion currentVersion = default)
            where TAggregate : AggregateRoot
        {
            if (currentVersion is { })
            {
                if (!currentVersion.IsNext(proposed.Version))
                {
                    throw new AggregateConflictDetectedException<TAggregate>(proposed.Id, currentVersion, proposed.Version);
                }
            }
            else if (!proposed.Version.IsNew)
            {
                throw new AggregateConflictDetectedException<TAggregate>(proposed.Id, proposed.Version);
            }
        }
    }
}