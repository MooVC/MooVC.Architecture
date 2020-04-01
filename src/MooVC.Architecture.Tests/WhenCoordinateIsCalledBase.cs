namespace MooVC.Architecture
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public abstract class WhenCoordinateIsCalledBase
    {
        [Fact]
        public void GivenAnEmptyOperationThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => Coordinate(null));
        }

        [Fact]
        public void GivenMultipleThreadsNoConcurrencyExceptionsAreThrown()
        {
            const int ExpectedCount = 5;

            int counter = 0;

            void Operation()
            {
                counter++;
            }

            Task[] tasks = CreateTasks(() => Coordinate(Operation), ExpectedCount);

            Task.WaitAll(tasks);

            Assert.Equal(ExpectedCount, counter);
        }

        [Fact]
        public void GivenMultipleThreadsWithATimeoutSetThenATimeoutExceptionIsThrownForAllBarOne()
        {
            const int ExpectedCount = 5;

            static void Operation()
            {
                Thread.Sleep(250);
            }

            Task[] tasks = CreateTasks(
                () => Coordinate(Operation, timeout: TimeSpan.FromMilliseconds(50)),
                ExpectedCount + 1);

            AggregateException exception = Assert.Throws<AggregateException>(() => Task.WaitAll(tasks));

            Assert.Equal(ExpectedCount, exception.InnerExceptions.Count);
        }

        protected abstract void Coordinate(Action operation, TimeSpan? timeout = default);

        protected Task[] CreateTasks(Action operation, int total)
        {
            var tasks = new List<Task>();

            for (int index = 0; index < total; index++)
            {
                tasks.Add(Task.Run(operation));
            }

            return tasks.ToArray();
        }
    }
}