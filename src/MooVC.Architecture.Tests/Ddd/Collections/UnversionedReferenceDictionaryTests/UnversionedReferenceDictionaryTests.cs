namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;

    public abstract class UnversionedReferenceDictionaryTests
    {
        public const int ExpectedCount = 2;

        protected UnversionedReferenceDictionaryTests()
        {
            FirstAggregate = new SerializableAggregateRoot();
            SecondAggregate = new SerializableAggregateRoot();
            FirstReference = FirstAggregate.ToReference();
            SecondReference = SecondAggregate.Id.ToReference<SerializableAggregateRoot>(version: SecondAggregate.Version.Next());

            Dictionary = new UnversionedReferenceDictionary<SerializableAggregateRoot, SerializableAggregateRoot>
            {
                { FirstReference, FirstAggregate },
                { SecondReference, SecondAggregate },
            };
        }

        public UnversionedReferenceDictionary<SerializableAggregateRoot, SerializableAggregateRoot> Dictionary { get; }

        public SerializableAggregateRoot FirstAggregate { get; }

        public Reference<SerializableAggregateRoot> FirstReference { get; }

        public SerializableAggregateRoot SecondAggregate { get; }

        public Reference<SerializableAggregateRoot> SecondReference { get; }
    }
}