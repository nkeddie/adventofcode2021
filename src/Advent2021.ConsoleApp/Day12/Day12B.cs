namespace Advent2021.ConsoleApp
{
    public class Day12B
    {
        private const string StartNode = "start";
        private const string EndNode = "end";

        public async Task<int> RunAsync_Simple()
        {
            var input = (await File.ReadAllLinesAsync(Path.Combine("Day12", "Input.txt")));

            var connections = new Dictionary<string, HashSet<string>>();

            foreach (var line in input)
            {
                var paths = line.Split('-');

                var current = paths[0];
                var link = paths[1];

                if (!connections.ContainsKey(current))
                    connections.Add(current, new HashSet<string>());

                connections[current].Add(link);

                if (!connections.ContainsKey(link))
                    connections.Add(link, new HashSet<string>());

                connections[link].Add(current);
            }

            var start = new List<(List<string>, bool)>
            {
                (new List<string> { StartNode }, false)
            };

            var end = new List<List<string>>();
            Traverse(connections, start, end);

            return end.Count;
        }

        private void Traverse(Dictionary<string, HashSet<string>> connections, List<(List<string> Paths, bool DidVisitCaveTwice)> current, List<List<string>> completedPaths)
        {
            var newPaths = new List<(List<string> Paths, bool DidVisitCaveTwice)>();

            foreach (var (Paths, DidVisitCaveTwice) in current)
            {
                var node = Paths.Last();

                var connectionsForNode = connections[node];

                foreach (var connection in connectionsForNode)
                {
                    var didVisitCaveTwice = DidVisitCaveTwice;

                    var newPath = new List<string>(Paths)
                    {
                        connection
                    };

                    if (connection == StartNode && Paths.Count > 1)
                        continue;

                    else if (connection[0] > 96 && Paths.Any(c => c == connection) && didVisitCaveTwice)
                        continue;

                    else if (connection == EndNode)
                        completedPaths.Add(newPath);

                    else
                    {
                        if (connection[0] > 96 && Paths.Any(c => c == connection))
                            didVisitCaveTwice = true;

                        newPaths.Add((newPath, didVisitCaveTwice));
                    }
                    
                }
            }

            if (newPaths.Count == 0) return;

            Traverse(connections, newPaths, completedPaths);
        }
    }
}
