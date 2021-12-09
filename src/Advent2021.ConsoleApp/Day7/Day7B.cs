namespace Advent2021.ConsoleApp
{
    public class Day7B
    {
        public async Task<int> RunAsync_Simple()
        {
            var input = (await File.ReadAllLinesAsync(Path.Combine("Day7", "Input.txt")))
                .First()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var maxFuel = input.Max();

            var fuelCost = int.MaxValue;

            for (var i = 0; i < maxFuel; i++)
            {
                var tmpFuelCost = 0;

                foreach (var x in input)
                {
                    var z = Enumerable.Range(0, Math.Abs(x - i) + 1);
                    tmpFuelCost += z.Sum();
                }
                if (tmpFuelCost < fuelCost) fuelCost = tmpFuelCost;
            }

            return fuelCost;
        }
    }
}
