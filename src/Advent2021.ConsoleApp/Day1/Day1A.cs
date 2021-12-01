using BenchmarkDotNet.Attributes;
using System.Buffers;
using System.Text;

namespace Advent2021.ConsoleApp;

[MemoryDiagnoser]
public class Day1A
{
    [Benchmark(Baseline = true)]
    public async Task<int> RunAsync_Simple()
    {
        var depths = (await File.ReadAllLinesAsync(Path.Combine("Day1", "Input.txt")))
            .Select(int.Parse)
            .ToList();

        return depths
            .Skip(1)
            .Where((d, i) => d > depths[i + 1 - 1])
            .Count();
    }

    [Benchmark]
    public async Task<int> RunAsync_Streaming()
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

    [Benchmark]
    public async Task<int> RunAsync_Streaming_LowAlloc()
    {
        using var fs = new FileStream(Path.Combine("Day1", "Input.txt"), FileMode.Open);

        fs.Seek(3, SeekOrigin.Begin);

        byte[] currentBytes = new byte[5];
        Memory<byte> buffer = new byte[1024];

        int lastIndex = 0;
        int lastDepth = int.MaxValue;
        int count = 0;

        bool continuation = false;
        int continuationIndex = 0;

        var totalBytesRead = int.MaxValue;

        while ((totalBytesRead = await fs.ReadAsync(buffer)) > 0)
        {
            lastIndex = 0;

            for (var i = 0; i < totalBytesRead; i++)
            {
                var @byte = buffer.Span[i];

                if (@byte == 13)
                {
                    int depth = 0;
                    if (continuation)
                    {
                        var subArray = buffer[lastIndex..i];
                        for (var j = 0; j < subArray.Length; j++)
                        {
                            currentBytes[j + continuationIndex] = subArray.Span[j];
                        }

                        depth = GetDepth(currentBytes, continuationIndex + subArray.Length);

                        continuation = false;
                        continuationIndex = 0;
                    }
                    else
                    {
                        buffer[lastIndex..i].CopyTo(currentBytes);
                        depth = GetDepth(currentBytes, i - lastIndex);
                    }

                    if (depth > lastDepth)
                    {
                        count++;
                    }

                    lastDepth = depth;
                    lastIndex = i + 1;
                }
                else if (@byte == 10)
                {
                    lastIndex++;
                }
                else if (i == totalBytesRead - 1)
                {
                    if (lastIndex > i) continue;
                    if (totalBytesRead < buffer.Length)
                    {
                        buffer.Slice(lastIndex, totalBytesRead - lastIndex).CopyTo(currentBytes);
                        var depth = GetDepth(currentBytes, totalBytesRead - lastIndex);

                        if (depth > lastDepth)
                        {
                            count++;
                        }

                        lastDepth = depth;
                        lastIndex = i + 1;
                    }
                    else
                    {
                        continuation = true;
                        continuationIndex = i + 1 - lastIndex;
                        buffer[lastIndex..].CopyTo(currentBytes);
                    }
                }
            }
        }

        return count;
    }

    private int GetDepth(Span<byte> bytes, int count)
    {
        int result = 0;

        for (var i = 0; i < count; i++)
        {
            result += (int)((bytes[i] - 48) * Math.Pow(10, count - i - 1));
        }

        return result;
    }
}
