namespace Advent2021.ConsoleApp
{
    public class Day8A
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
                var (_, output) = l;

                var outputSignals = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var result = 0;

                foreach (var s in outputSignals)
                {
                    if (s.Length == 2)
                        result++;

                    else if (s.Length == 4)
                        result++;

                    else if (s.Length == 3)
                        result++;

                    else if (s.Length == 7)
                        result++;
                }

                return result;
            });

            return sum;
        }
    }
}
