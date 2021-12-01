using System.Diagnostics;

namespace Advent2021.ConsoleApp
{
    internal class PerformanceOutput<T>
    {
        private readonly List<(T Output, long Elapsed)> runs;

        public PerformanceOutput(List<(T, long)> runs)
        {
            this.runs = runs ?? throw new ArgumentNullException(nameof(runs));
        }

        public long TotalMilliseconds => runs.Sum(s => s.Elapsed);
        public double AverageMilliseconds => runs.Average(s => s.Elapsed);
        public T Output => runs.First().Output;

        public override string ToString()
        {
            return $"Output: {Output}, Total (ms): {TotalMilliseconds}, Average (ms): {AverageMilliseconds}";
        }
    }

    internal static class Performance
    {
        internal static async Task<PerformanceOutput<T>> TimeAsync<T>(Func<Task<T>> action, int runCount = 1)
        {
            var timingsPerRun = new List<(T, long)>();

            var watch = new Stopwatch();

            for (var i = 0; i < runCount; i++)
            {
                watch.Restart();
                T result = await action();
                watch.Stop();
                timingsPerRun.Add((result, watch.ElapsedMilliseconds));
            }

            return new PerformanceOutput<T>(timingsPerRun);
        }
    }
}
