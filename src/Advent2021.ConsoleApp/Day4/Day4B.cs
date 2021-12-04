namespace Advent2021.ConsoleApp
{
    public class Day4B
    {
        public async Task<int> RunAsync_Simple()
        {
            var input = (await File.ReadAllLinesAsync(Path.Combine("Day4", "Input.txt")));

            var calls = input
                .First()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var boards = input.Skip(2)
                .Where(l => !string.IsNullOrEmpty(l))
                .Select((l, i) => (Index: i, Row: l.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()))
                .GroupBy(l => l.Index / 5)
                .Select(l => l.Select(l => l.Row).ToArray())
                .Select(l => new BingoBoard(l))
                .ToList();

            var winners = new Dictionary<Guid, (int, int)>();

            foreach (var call in calls)
            {
                foreach (var board in boards)
                {
                    board.HandleCalledNumber(call);

                    if (!winners.ContainsKey(board.Identifier) && board.IsWinner())
                    {
                        var unmarked = board.SumUnmarked(); // 4A
                        winners.Add(board.Identifier, (call, unmarked));
                    }
                }
            }

            var (identifier, kvp) = winners.Last();
            var (sum, callNumber) = kvp;

            return sum * callNumber;
        }

        public class BingoBoard
        {
            private const int SIZE = 5;
            private const int MARKED_NUMBER = -1;

            private readonly int[][] board;

            public BingoBoard(int[][] board)
            {
                this.board = board;
                Identifier = Guid.NewGuid();
            }

            public Guid Identifier { get; set; }

            public bool IsWinner()
            {
                for (var i = 0; i < board.Length; i++)
                {
                    var hitCountX = 0;
                    var hitCountY = 0;

                    for (var j = 0; j < board.Length; j++)
                    {
                        if (board[i][j] == MARKED_NUMBER) hitCountX++;
                        if (board[j][i] == MARKED_NUMBER) hitCountY++;

                    }

                    if (hitCountX == SIZE || hitCountY == SIZE) return true;
                }

                return false;
            }

            public void HandleCalledNumber(int number)
            {
                for (var i = 0; i < board.Length; i++)
                {
                    for (var j = 0; j < board.Length; j++)
                    {
                        if (board[i][j] == number)
                        {
                            board[i][j] = MARKED_NUMBER;
                        }
                    }
                }
            }

            public int SumUnmarked()
            {
                var sum = 0;

                for (var i = 0; i < board.Length; i++)
                {
                    for (var j = 0; j < board.Length; j++)
                    {
                        var value = board[i][j];

                        if (value != MARKED_NUMBER)
                        {
                            sum += value;
                        }
                    }
                }

                return sum;
            }
        }
    }
}
