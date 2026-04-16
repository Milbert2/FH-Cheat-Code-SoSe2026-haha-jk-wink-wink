using FHS.CT.AlgoDat.DataStructures;

if (args.Length != 1)
{
    Console.WriteLine("Usage: dotnet run -- <input-file>");

    return;
}

var solver = new AoCSolverDay12(args[0]);
Console.WriteLine($"Result: {solver.NumberOfPaths()}");

public class AoCSolverDay12
{
    private Graph<string> _graph;

    public AoCSolverDay12(string input)
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

    public int NumberOfPaths()
    {
        var tree = new AoCTree();
        var startNode = new AoCTree.Node("start");
        tree.Root = startNode;
        ExpandTree(tree, startNode);

        return tree.CountPaths();
    }

    public void ExpandTree(AoCTree tree, AoCTree.Node node)
    {
        if (node.Value == "end")
        {
            return;
        }

        foreach (var edge in _graph.GetEdges(node.Value)!)
        {
            if (CanVisitMultipleTimes(edge) || !tree.ContainsNode(node, edge))
            {
                var edgeNode = tree.AddChild(node, edge);
                ExpandTree(tree, edgeNode);
            }
        }
    }

    public bool CanVisitMultipleTimes(string nodeName)
    {
        return nodeName.ToUpper() == nodeName;
    }
}

public class AoCTree
{
    public class Node
    {
        public string Value { get; }
        public Node? Parent { get; }

        public List<Node> Children { get; }

        public Node(string value, Node? parent = null)
        {
            Value = value;
            Parent = parent;
            Children = new();
        }
    }

    public Node? Root { get; set; }

    public Node AddChild(Node parent, string nodeName)
    {
        var children = parent.Children;
        var node = new Node(nodeName, parent);
        children.Add(node);

        return node;
    }

    public bool ContainsNode(Node startAt, string nodeName)
    {
        Node? currentNode = startAt;
        while (currentNode != null)
        {
            if (currentNode.Value == nodeName)
            {
                return true;
            }

            currentNode = currentNode.Parent;
        }

        return false;
    }

    public int CountPaths()
    {
        if (Root == null)
        {
            return 0;
        }

        return CountPaths(Root);
    }

    private int CountPaths(Node node)
    {
        if (node.Value == "end" && node.Children.Count == 0)
        {
            return 1;
        }

        int paths = 0;
        foreach (var child in node.Children)
        {
            paths += CountPaths(child);
        }

        return paths;
    }
}
