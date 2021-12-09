namespace Advent2021.ConsoleApp
{
    public class Day6A
    {
        public async Task<long> RunAsync_Simple()
        {
            const int ITERATIONS = 80;

            var lines = await File.ReadAllLinesAsync(Path.Combine("Day6", "Input.txt"));

            var buckets = Enumerable.Range(0, 9)
              .ToDictionary(l => l, l => 0L);

            var lanternFish = lines.First()
              .Split(",", StringSplitOptions.RemoveEmptyEntries)
              .Select(int.Parse);

            foreach (var fish in lanternFish)
            {
                buckets[fish]++;
            }

            for (int i = 0; i < ITERATIONS; i++)
            {
                var bucketZero = buckets[0];

                for (int j = 0; j < buckets.Count; j++)
                {
                    if (j == 6)
                        buckets[j] = buckets[j + 1] + bucketZero;

                    else if (j == 8)
                        buckets[j] = bucketZero;

                    else
                        buckets[j] = buckets[j + 1];
                }
            }

            return buckets.Sum(b => b.Value);
        }
    }
}
