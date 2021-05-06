namespace MooVC.Architecture.Ddd.Services.CoordinatedGenerateHandlerTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;

    internal sealed class TestableCoordinatedGenerateHandler<TCommand>
        : CoordinatedGenerateHandler<AggregateRoot, TCommand>
        where TCommand : Message
    {
        public TestableCoordinatedGenerateHandler(
            IRepository<AggregateRoot> repository,
            TimeSpan? timeout = default)
            : base(repository, timeout)
        {
        }

        protected override AggregateRoot PerformCoordinatedGenerate(TCommand command)
        {
            return new SerializableEventCentricAggregateRoot(command.Id);
        }
    }
}