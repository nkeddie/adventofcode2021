namespace Advent2021.ConsoleApp;
public static class Day1B
{
    const int SlidingWindowSize = 3;

    public static async Task<int> RunAsync()
    {
        using var file = File.OpenRead(Path.Combine("Day1", "Input.txt"));
        using var reader = new StreamReader(file);

        var recent = Enumerable.Repeat(0, SlidingWindowSize).Select(async _ =>
        {
            return await ReadIntAsync(reader);
        })
        .Select(t => t.Result)
        .ToList();

        var count = 0;
        var lastSum = recent.Sum();

        while (!reader.EndOfStream)
        {
            recent.Add(await ReadIntAsync(reader));
            recent.RemoveAt(0);

            var currentSum = recent.Sum();

            if (currentSum > lastSum)
                count++;
            
            lastSum = currentSum;
        }

        return count;
    }

    private static async Task<int> ReadIntAsync(StreamReader reader)
    {
        var raw = await reader.ReadLineAsync() ?? throw new Exception("Failed to read input");
        return int.Parse(raw);
    }
}
