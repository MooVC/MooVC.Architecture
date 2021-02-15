namespace MooVC.Architecture
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public abstract class WhenCoordinateIsCalledBase
    {
        [Fact]
        public void GivenAnEmptyOperationThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => Coordinate(default!));
        }

        [Fact]
        public async Task GivenAnEmptyOperationThenAnArgumentNullExceptionIsThrownAsync()
        {
            _ = await Assert.ThrowsAsync<ArgumentNullException>(() => CoordinateAsync(default!));
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
        public async Task GivenMultipleThreadsNoConcurrencyExceptionsAreThrownAsync()
        {
            const int ExpectedCount = 5;

            int counter = 0;

            Task Operation()
            {
                counter++;

                return Task.CompletedTask;
            }

            Task[] tasks = CreateTasks(() => CoordinateAsync(Operation), ExpectedCount);

            await Task.WhenAll(tasks);

            Assert.Equal(ExpectedCount, counter);
        }

        protected abstract void Coordinate(Action operation, TimeSpan? timeout = default);

        protected abstract Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default);

        private static Task[] CreateTasks(Action operation, int total)
        {
            return CreateTasks(() => Task.Run(operation), total);
        }

        private static Task[] CreateTasks(Func<Task> operation, int total)
        {
            var tasks = new List<Task>();

            for (int index = 0; index < total; index++)
            {
                tasks.Add(operation());
            }

            return tasks.ToArray();
        }
    }
}