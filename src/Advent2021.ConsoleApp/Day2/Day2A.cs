namespace Advent2021.ConsoleApp
{
    public class Day2A
    {
        public async Task<int> RunAsync_Simple()
        {
            var input = (await File.ReadAllLinesAsync(Path.Combine("Day2", "Input.txt")));

            var forward = 0;
            var depth = 0;

            foreach (var line in input)
            {
                var tokens = line.Split(" ");
                var direction = tokens[0].Trim();
                var amount = int.Parse(tokens[1].Trim());


                switch (direction)
                {
                    case "forward":
                        forward += amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                }
            }

            return forward * depth;
        }
    }
}
