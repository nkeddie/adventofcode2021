
namespace Advent2021.ConsoleApp
{
    public class Day11A
    {
        private const int iterations = 100;

        public async Task<int> RunAsync_Simple()
        {
            var matrix = (await File.ReadAllLinesAsync(Path.Combine("Day11", "Input.txt")))
                .Select(line => line.Select(x => x - 48).ToArray())
                .ToArray();

            int flashCount = 0;

            for (var iter = 0; iter < iterations; iter++)
            {
                var flashes = new List<(int row, int col)>();

                for (var row = 0; row < matrix.Length; row++)
                {
                    for (var col = 0; col < matrix[row].Length; col++)
                    {
                        var value = matrix[row][col] + 1;

                        if (value > 9)
                        {
                            flashes.Add((row, col));
                        }

                        matrix[row][col] = value;
                    }
                }

                var directions = new List<(int x, int y)>
                {
                    (-1, -1),
                    ( 0, -1),
                    ( 1, -1),
                    ( 1,  0),
                    ( 1,  1),
                    ( 0,  1),
                    (-1,  1),
                    (-1,  0)
                };

                foreach (var cell in flashes)
                {
                    flashCount += Flash(matrix, cell, directions);
                }

                for (var row = 0; row < matrix.Length; row++)
                {
                    for (var col = 0; col < matrix[row].Length; col++)
                    {
                        var value = matrix[row][col];

                        if (value > 9)
                        {
                            matrix[row][col] = 0;
                        }
                    }
                }
            }

            return flashCount;
        }

        private int Flash(int[][] matrix, (int row, int col) cell, List<(int x, int y)> directions)
        {
            var flashCount = 1;

            foreach (var (x, y) in directions)
            {
                var row = cell.row + x;
                var col = cell.col + y;

                if (row < 0) continue;
                if (col < 0) continue;
                if (row >= matrix.Length) continue;
                if (col >= matrix.Length) continue;

                matrix[row][col]++;

                if (matrix[row][col] == 10)
                {
                    flashCount += Flash(matrix, (row, col), directions);
                }
            }

            return flashCount;
        }
    }
}