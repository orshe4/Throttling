using System;
using System.Threading;
using System.Threading.Tasks;

namespace Throttling
{
    public class Throttled
    {
        private readonly SemaphoreSlim _throttler;

        public int TaskLimit { get; }
        public int LimitingPeriodInMilliseconds { get; }

        /// <summary>
        /// Create a Throttled instance
        /// </summary>
        /// <param name="taskLimit">The task limit to be done in the specific period</param>
        /// <param name="limitingPeriodInSeconds">The limiting period in milliseconds</param>
        public Throttled(int taskLimit, int limitingPeriodInMilliseconds)
        {
            TaskLimit = taskLimit;
            LimitingPeriodInMilliseconds = limitingPeriodInMilliseconds;
            _throttler = new SemaphoreSlim(TaskLimit);

        }

        public async Task Run(Func<Task> function)
        {
            await _throttler.WaitAsync();

            Task task = function.Invoke();

            _ = Task.Delay(LimitingPeriodInMilliseconds)
                .ContinueWith((t) => _throttler.Release());

            await task;
        }

        public async Task<TResult> Run<TResult>(Func<Task<TResult>> function)
        {
            await _throttler.WaitAsync();

            Task<TResult> task = function.Invoke();

            _ = Task.Delay(LimitingPeriodInMilliseconds)
                .ContinueWith((t) => _throttler.Release());

            return await task;
        }
    }
}
