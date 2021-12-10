using System;

namespace Advent2021.ConsoleApp
{
    public class Day9A
    {
        public async Task<int> RunAsync_Simple()
        {
            var matrix = (await File.ReadAllLinesAsync(Path.Combine("Day9", "Input.txt")))
                .Select(l => l.Select(x => x - 48).ToArray())
                .ToArray();

            if (matrix == null) throw new Exception("Invalid input");

            static int Get(int[][] matrix, int row, int col, (int x, int y) direction)
            {
                try
                {
                    return matrix[row + direction.x][col + direction.y];
                }
                catch (IndexOutOfRangeException)
                {
                    return int.MaxValue;
                }
            }

            var dict = new Dictionary<(int x, int y), int>();

            for (var row = 0; row < matrix.Length; row++)
            {
                for (var col = 0; col < matrix[row].Length; col++)
                {
                    var element = matrix[row][col];
       
                    Console.ForegroundColor = ConsoleColor.White;

                    var left = Get(matrix, row, col, (0, -1));
                    var right = Get(matrix, row, col, (0, 1));
                    var top = Get(matrix, row, col, (-1, 0));
                    var bottom = Get(matrix, row, col, (1, 0));

                    bool basin = false;

                    if (element < left && element < right && element < top && element < bottom)
                        basin = true;

                    if (basin)
                    {
                        dict.Add((col, row), element + 1);
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.Write(element);

                }

                Console.WriteLine();
            }

            return dict.Sum(x => x.Value);
        }
    }
}
