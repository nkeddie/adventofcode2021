using BenchmarkDotNet.Attributes;
using System.Collections;

namespace Advent2021.ConsoleApp;

[MemoryDiagnoser]
public class Day3A
{
    [Benchmark(Baseline = true)]
    public async Task<int> RunAsync_Simple()
    {
        var lines = (await File.ReadAllLinesAsync(Path.Combine("Day3", "Input.txt")));

        var binary = lines
            .SelectMany(l => l
                .ToCharArray()
                .Select((l, i) => (index: i, element: l == '1' ? 1 : -1))
            )
            .GroupBy(l => l.index)
            .Select(l => l.Sum(x => x.element) > 0)
            .Reverse()
            .ToArray();

        var bits = new BitArray(binary);
        var bits2 = new BitArray(binary).Not();

        var result = new int [2];
        bits.CopyTo(result, 0);
        bits2.CopyTo(result, 1);

        return result[0] * result[1];
    }
}
