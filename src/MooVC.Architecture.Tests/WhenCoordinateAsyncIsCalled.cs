namespace MooVC.Architecture
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public abstract class WhenCoordinateAsyncIsCalled
    {
        [Fact]
        public async Task GivenAnEmptyOperationThenAnArgumentNullExceptionIsThrownAsync()
        {
            _ = await Assert.ThrowsAsync<ArgumentNullException>(() => CoordinateAsync(default!));
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

        protected abstract Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default);

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