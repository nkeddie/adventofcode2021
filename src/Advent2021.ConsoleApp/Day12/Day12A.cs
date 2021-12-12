namespace Advent2021.ConsoleApp
{
    public class Day12A
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

            return Traverse(connections, StartNode, new List<string>());
        }

        private int Traverse(Dictionary<string, HashSet<string>> connections, string node, List<string> currentPath)
        {
            var connectionsForNode = connections[node];

            if (node[0] > 96 && currentPath.Contains(node))
            {
                return 0;
            }

            if (node == EndNode)
            {
                return 1;
            }

            currentPath.Add(node);

            var validPathCount = 0;

            foreach (var connection in connectionsForNode)
            {
                var path = new List<string>(currentPath);

                validPathCount += Traverse(connections, connection, path);

            }

            return validPathCount;
        }
    }
}
