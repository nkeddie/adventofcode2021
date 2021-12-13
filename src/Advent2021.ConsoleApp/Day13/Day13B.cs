namespace Advent2021.ConsoleApp
{
    public class Day13B
    {
        public async Task<int> RunAsync_Simple()
        {
            var input = await File.ReadAllLinesAsync(Path.Combine("Day13", "Input.txt"));

            var coords = input
                .TakeWhile(l => l.Length > 0)
                .Select(l => {
                    var coords = l.Split(',');
                    return (x: int.Parse(coords[0]), y: int.Parse(coords[1]));
                })
                .ToArray();

            var instructions = input.SkipWhile(l => l.Length > 0)
                .Skip(1)
                .Select(l =>
                {
                    var directions = l.Split(" ")[2];
                    var axis = directions.Split("=")[0][0];
                    var line = int.Parse(directions.Split("=")[1]);

                    return (axis, line);
                });

            foreach (var instruction in instructions)
            {
                for (var i = 0; i < coords.Length; i++)
                {
                    var (x, y) = coords[i];

                    if (instruction.axis == 'x' && x > instruction.line)
                    {
                        coords[i].x = instruction.line * 2 - x;
                    }
                    else if (instruction.axis == 'y' && y > instruction.line)
                    {
                        coords[i].y = instruction.line * 2 - y;
                    }
                }
            }

            var width = coords.Max(c => c.x);
            var height = coords.Max(c => c.y);

            var result = coords.Distinct().ToHashSet();


            for (var row = 0; row <= height; row++)
            {
                for (var col = 0; col <= width; col++)
                {
                    if (result.Contains((col, row)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('#');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write('.');
                    }
                }

                Console.WriteLine();
            }

            return 0;
        }
    }
}
