namespace Advent2021.ConsoleApp
{
    public class Day9B
    {
        private static Random random = new ();
        public async Task<int> RunAsync_Simple()
        {
            var matrix = (await File.ReadAllLinesAsync(Path.Combine("Day9", "Input.txt")))
                .Select(l => l.Select(x => x - 48).ToArray())
                .ToArray();

            if (matrix == null) throw new Exception("Invalid input");

            var lowPoints = new Dictionary<(int x, int y), int>();

            for (var row = 0; row < matrix.Length; row++)
            {
                for (var col = 0; col < matrix[row].Length; col++)
                {
                    var element = matrix[row][col];

                    var left = Get(matrix, row, col, (0, -1));
                    var right = Get(matrix, row, col, (0, 1));
                    var top = Get(matrix, row, col, (-1, 0));
                    var bottom = Get(matrix, row, col, (1, 0));

                    if (element < left && element < right && element < top && element < bottom)
                        lowPoints.Add((col, row), element);
                }
            }

            var basins = new Dictionary<(int, int), HashSet<(int, int)>>();

            foreach (var lowPoint in lowPoints)
            {
                var basin = new HashSet<(int, int)>();
                Recurse(matrix, lowPoint.Key, lowPoint.Value, basin);
                basins.Add(lowPoint.Key, basin);
            }

            return basins
                .OrderByDescending(k => k.Value.Count)
                .Take(3)
                .Select(k => k.Value.Count)
                .Aggregate((a, b) => a * b);
        }
        private static int Get(int[][] matrix, int row, int col, (int x, int y) direction)
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
        private static void Recurse(int[][] matrix, (int col, int row) index, int value, HashSet<(int, int)> tracked)
        {
            int element;
            
            try
            {
                element = matrix[index.row][index.col];
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }

            if (element == 9) return;
            if (element < value) return;


            tracked.Add(index);

            Recurse(matrix, (index.col, index.row - 1), element + 1, tracked);
            Recurse(matrix, (index.col, index.row + 1), element + 1, tracked);
            Recurse(matrix, (index.col - 1, index.row), element + 1, tracked);
            Recurse(matrix, (index.col + 1, index.row), element + 1, tracked);
        }
    }
}
