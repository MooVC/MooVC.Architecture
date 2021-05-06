namespace MooVC.Architecture.ValueTests
{
    using System.Collections.Generic;

    public sealed class EmptyValue
        : Value
    {
        public EmptyValue()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield break;
        }
    }
}