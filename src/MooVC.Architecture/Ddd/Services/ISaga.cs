namespace MooVC.Architecture.Ddd.Services;

public interface ISaga<TStart, TStop>
    : IStartSaga<TStart>,
      IStopSaga<TStop>
    where TStart : DomainEvent
    where TStop : DomainEvent
{
}