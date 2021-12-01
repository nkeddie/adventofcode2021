namespace Advent2021.ConsoleApp;
public static class Day1A
{
    public static async Task<int> RunAsync()
    {
        using var file = File.OpenRead(Path.Combine("Day1", "Input.txt"));
        using var reader = new StreamReader(file);

        var count = 0;
        var lastLine = int.MaxValue;

        while (!reader.EndOfStream)
        {
            var raw = await reader.ReadLineAsync() ?? throw new ArgumentNullException();

            var x = int.Parse(raw);

            if (x > lastLine)
                count++;

            lastLine = x;
        }

        return count;
    }
}
