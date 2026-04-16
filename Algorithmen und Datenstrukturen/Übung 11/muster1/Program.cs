using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

if (args.Length != 1)
{
    Console.WriteLine("Usage: dotnet run -- <input-file>");

    return;
}

var validator = new GraphValidator(args[0]);

Console.WriteLine($"Result: {validator.IsTree()}");

public class GraphValidator
{
    private Graph<string> _graph;

    public GraphValidator(string input)
    {
        _graph = ReadGraph(input);
    }

    private static Graph<string> ReadGraph(string inputFile)
    {
        var graph = new Graph<string>();

        var lines = File.ReadLines(inputFile);
        foreach (var line in lines)
        {
            var nodes = line.Split("-");
            graph.AddEdge(nodes[0], nodes[1]);
            graph.AddEdge(nodes[1], nodes[0]); // reverse edge
        }

        return graph;
    }

    public bool IsTree()
    {
        var startNode = _graph.First();

        var isConnected = IsConnected(startNode);
        if (!isConnected)
        {
            return false;
        }

        var hasCycle = HasCycle();

        return !hasCycle; // if there is no cycle: graph IS connected AND has NO cycle => it's a tree
    }

    private bool HasCycle()
    {
        // we choose a Kruskal style approach: Start with each node in its own set.
        // Inspect each edge and try to merge sets if both endings. If we find two neighboring nodes already in the
        // same set we have found a cycle
        UnionFind<string> uf = new();
        List<(string, string)> edges = new();
        foreach (var node in _graph)
        {
            uf.MakeSet(node);

            foreach (var neighbor in _graph.GetEdges(node)!)
            {
                // our problem: Within the graph we store n1 -> n2 AND n2 -> n1 separately.
                // if we strictly follow the approach above, we will find each edge "twice" and assume a
                // cycle falsely. To prevent us from this situation we only save one direction in the edges
                // list. We save only edges where n1 is "smaller" than n2
                if (node.CompareTo(neighbor) < 0)
                {
                    edges.Add((node, neighbor));
                }
            }
        }

        foreach (var (n1, n2) in edges)
        {
            var n1Ambassador = uf.Find(n1);
            var n2Ambassador = uf.Find(n2);

            if (n1Ambassador.CompareTo(n2Ambassador) != 0)
            {
                uf.Union(n1, n2);
            }
            else
            {
                return true; // n1 and n2 are in the same set. This means we found a cycle
            }
        }

        return false;
    }

    private bool IsConnected(string startNode)
    {
        var visitedNodes = new List<string>();
        BreadthFirstSearch<string>.ProcessNode nodeProcessor = node =>
        {
            visitedNodes.Add(node);
        };

        // check for connectedness
        BreadthFirstSearch<string>.Travers(_graph, startNode, nodeProcessor);

        // if our BFS found (less) nodes than our graph has in total: This means we cannot reach each node from every start point => our graph is not connected
        return visitedNodes.Count == _graph.Count();
    }
}
