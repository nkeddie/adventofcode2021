
namespace Advent2021.ConsoleApp
{
    public class Day10A
    {
        public async Task<int> RunAsync_Simple()
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
                { ']', 57  },
                { ')', 3 },
                { '>', 25137 },
                { '}', 1197 }
            };

            var scoreDictionary = new Dictionary<char, int>()
            {
                { ']', 0 },
                { ')', 0 },
                { '>', 0 },
                { '}', 0 }
            };
            foreach (var line in lines)
            {
                var stack = new Stack<char>();

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
                            scoreDictionary[@char]++;
                            break;
                        }
                    }
                }
            }

            return scoreDictionary.Sum(d => d.Value * scoreLookup[d.Key]);
        }
    }
}