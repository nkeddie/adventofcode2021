namespace Advent2021.ConsoleApp
{
    public class Day5A
    {
        public async Task<int> RunAsync_Simple()
        {
            var input = (await File.ReadAllLinesAsync(Path.Combine("Day5", "Input.txt")));

            var grid = new Dictionary<(int X, int Y), int>();

            foreach (var line in input)
            {
                var parsed = line.Split("->");
                var (startX, startY) = ParseLineSegment(parsed[0]);
                var (endX, endY) = ParseLineSegment(parsed[1]);

                if (startX == endX || startY == endY)
                {
                    var signX = Math.Sign(endX - startX);
                    var signY = Math.Sign(endY - startY);

                    var current = (X: startX, Y: startY);

                    do
                    {
                        if (!grid.ContainsKey(current))
                        {
                            grid[current] = 0;
                        }

                        grid[current]++;

                        current.X += signX;
                        current.Y += signY;


                        if (current == (endX + signX, endY + signY))
                            break;
                    }
                    while (true);
                }

            }

            return grid.Count(g => g.Value >= 2);
        }

        public static (int X, int Y) ParseLineSegment(string raw)
        {
            var parsed = raw.Split(",");
            return (int.Parse(parsed[0]), int.Parse(parsed[1]));
        }
    }
}
