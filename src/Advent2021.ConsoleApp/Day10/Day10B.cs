
namespace Advent2021.ConsoleApp
{
    public class Day10B
    {
        public async Task<long> RunAsync_Simple()
        {
            var lines = await File.ReadAllLinesAsync(Path.Combine("Day10", "Input.txt"));

            var dictionary = new Dictionary<char, char>()
            {
                { '[', ']' },
                { '(', ')' },
                { '<', '>' },
                { '{', '}' }
            };

            var scoreLookup = new Dictionary<char, int>
            {
                { '[', 2  },
                { '(', 1 },
                { '<', 4 },
                { '{', 3 }
            };

            var scores = new List<long>();

            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                var valid = true;

                foreach (var @char in line)
                {
                    if (dictionary.ContainsKey(@char))
                    {
                        stack.Push(@char);
                    }
                    else
                    {
                        stack.TryPeek(out char top);

                        if (dictionary[top] == @char)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                }

                if (valid)
                {
                    var score = 0L;
                    while (stack.TryPop(out var @char))
                    {
                        score *= 5;
                        score += scoreLookup[@char];
                    }

                    scores.Add(score);
                }
            }

            var arr = scores.OrderBy(s => s).ToArray();
            return arr.ElementAt(arr.Length / 2);
        }
    }
}