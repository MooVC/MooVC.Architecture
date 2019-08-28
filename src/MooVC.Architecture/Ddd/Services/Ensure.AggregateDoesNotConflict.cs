namespace MooVC.Architecture.Ddd.Services
{
    public static class Ensure
    {
        public static void AggregateDoesNotConflict<TAggregate>(AggregateRoot proposed, ulong? currentVersion = default)
            where TAggregate : AggregateRoot
        {
            if (currentVersion.HasValue)
            {
                if (proposed.Version - currentVersion != 1)
                {
                    throw new AggregateConflictDetectedException<TAggregate>(proposed.Id, currentVersion.Value, proposed.Version);
                }
            }
            else if (proposed.Version != AggregateRoot.DefaultVersion)
            {
                throw new AggregateConflictDetectedException<TAggregate>(proposed.Id, proposed.Version);
            }
        }
    }
}