using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Throttling;
using Xunit;

namespace Lib.Tests
{
    public class ThrottledTests
    {
        [Theory]
        [InlineData(30, 200, 10)]
        [InlineData(100, 1000, 5)]
        public async Task Run_ExecutionCounterInRange(int taskLimit, int limitingPeriodInMilliseconds, int testPeriodRounds)
        {
            Throttled throttled = new Throttled(taskLimit, limitingPeriodInMilliseconds);
            Stopwatch stopwatch = new Stopwatch();
            int testPeriodInMilliseconds = limitingPeriodInMilliseconds * testPeriodRounds;
            int expectedCounter = taskLimit * testPeriodRounds;
            int counter = 0;

            stopwatch.Start();

            while (stopwatch.Elapsed < TimeSpan.FromMilliseconds(testPeriodInMilliseconds))
            {
                await throttled.Run(() => Task.CompletedTask);

                if (stopwatch.Elapsed < TimeSpan.FromMilliseconds(testPeriodInMilliseconds))
                {
                    Interlocked.Increment(ref counter);
                }
            }

            Assert.Equal(expectedCounter, counter);
        }
    }
}
