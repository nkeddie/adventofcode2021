namespace Advent2021.ConsoleApp
{
    public class Day7A
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
                    tmpFuelCost += Math.Abs(x - i);
                }
                if (tmpFuelCost < fuelCost) fuelCost = tmpFuelCost;
            }

            return fuelCost;
        }
    }
}
