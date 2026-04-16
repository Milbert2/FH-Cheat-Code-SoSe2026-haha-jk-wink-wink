using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

if (args.Length != 1)
{
    Console.WriteLine("Usage: dotnet run -- <input file>");
    return;
}

string[] allLines = File.ReadAllLines(args[0]);
int[,] riskMap = new int[allLines[0].Length, allLines.Length];
for (int y = 0; y < allLines.Length; y++)
{
    for (int x = 0; x < allLines[y].Length; x++)
    {
        riskMap[x, y] = int.Parse(allLines[y][x] + "");
    }
}

var graph = BuildGraph(riskMap);

(int, int) start = (0, 0);
(int, int) end = (riskMap.GetLength(0) - 1, riskMap.GetLength(1) - 1);
List<(int, int)> path = ShortestPath<(int, int)>.Compute(graph, start, end);
double costs = ComputeCosts(graph, path);
Console.WriteLine($"Risk level is at {costs}");

double ComputeCosts(WeightedGraph<(int, int)> graph, List<(int, int)> path)
{
    double costs = 0;

    path.Reverse();
    for (int i = 0; i < path.Count - 1; i++)
    {
        var from = path[i];
        var to = path[i + 1];

        var edges = graph.GetEdges(from)!;
        // ugly way of finding costs of one edge
        foreach (var edge in edges)
        {
            if (edge.To == to)
            {
                costs += edge.Weight;

                break;
            }
        }
    }

    return costs;
}

WeightedGraph<(int, int)> BuildGraph(int[,] map)
{
    var graph = new WeightedGraph<(int, int)>();

    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            foreach ((int, int) neighbor in ComputeNeighbors((x, y), map.GetLength(0) - 1, map.GetLength(1) - 1))
            {
                graph.AddEdge((x, y), neighbor, map[x, y]);
            }
        }
    }

    return graph;
}

List<(int, int)> ComputeNeighbors((int, int) p, int maxX, int maxY)
{
    List<(int, int)> neighbors = new();

    // left neighbor
    if (p.Item1 > 0)
    {
        neighbors.Add((p.Item1 - 1, p.Item2));
    }

    // top neighbor
    if (p.Item2 > 0)
    {
        neighbors.Add((p.Item1, p.Item2 - 1));
    }

    // right neighbor
    if (p.Item1 < maxX)
    {
        neighbors.Add((p.Item1 + 1, p.Item2));
    }

    // bottom neighbor
    if (p.Item2 < maxY)
    {
        neighbors.Add((p.Item1, p.Item2 + 1));
    }

    return neighbors;
}