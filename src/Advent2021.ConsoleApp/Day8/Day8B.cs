namespace Advent2021.ConsoleApp
{
    public class Day8B
    {
        public async Task<int> RunAsync_Simple()
        {
            var lines = (await File.ReadAllLinesAsync(Path.Combine("Day8", "Input.txt")))
               .Select(l => {
                   var split = l.Split('|', StringSplitOptions.RemoveEmptyEntries);
                   return (split[0], split[1]);
               });

            var sum = lines.Sum(l =>
            {
                var (input, output) = l;

                var outputSignals = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var inputSignals = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var one = inputSignals.First(s => s.Length == 2).ToHashSet();
                var four = inputSignals.First(s => s.Length == 4).ToHashSet();
                var seven = inputSignals.First(s => s.Length == 3).ToHashSet();
                var eight = inputSignals.First(s => s.Length == 7).ToHashSet();

                var result = "";

                static bool test(string signal, HashSet<char> set, int expected)
                {
                    var count = 0;

                    foreach (var c in signal)
                    {
                        if (set.Contains(c))
                            count++;
                    }

                    return count == expected;
                }

                foreach (var s in outputSignals)
                {
                    // 0 = 2/2 1 + 3/4 4 + 3/3 7 + 6/7 8
                    // 1 = LEN 2
                    // 2 = 1/2 1 + 2/4 4 + 2/3 7 + 5/7 8
                    // 3 = 2/2 1 + 3/4 4 + 3/3 7 + 5/7 8
                    // 4 = LEN 4
                    // 5 = 1/2 1 + 3/4 4 + 2/3 7 + 5/7 8
                    // 6 = X/2 1 + X/4 4 + X/3 7 + X/7 8
                    // 7 = LEN 3
                    // 8 = LEN 7
                    // 9 = 2/2 1 + 4/4 4 + 3/3 7 + 6/7 8

                    if (test(s, one, 2) && test(s, four, 3) && test(s, seven, 3) && test(s, eight, 6))
                        result += "0";

                    else if (s.Length == 2)
                        result += "1";

                    else if (test(s, one, 1) && test(s, four, 2) && test(s, seven, 2) && test(s, eight, 5))
                        result += "2";

                    else if (test(s, one, 2) && test(s, four, 3) && test(s, seven, 3) && test(s, eight, 5))
                        result += "3";

                    else if (s.Length == 4)
                        result += "4";

                    else if (test(s, one, 1) && test(s, four, 3) && test(s, seven, 2) && test(s, eight, 5))
                        result += "5";

                    else if (test(s, one, 1) && test(s, four, 3) && test(s, seven, 2) && test(s, eight, 6))
                        result += "6";

                    else if (s.Length == 3)
                        result += "7";

                    else if (s.Length == 7)
                        result += "8";

                    else if (test(s, one, 2) && test(s, four, 4) && test(s, seven, 3) && test(s, eight, 6))
                        result += "9";

                    else throw new Exception("Unable to determine signal for " + s);
                }

                return int.Parse(result);
            });

            return sum;
        }
    }
}