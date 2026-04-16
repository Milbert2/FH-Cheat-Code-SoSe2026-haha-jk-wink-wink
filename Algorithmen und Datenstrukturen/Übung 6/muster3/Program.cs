using FHS.CT.AlgoDat.DataStructures;

var tree = new AvlTree<int>();

var r = new Random();
for (int i = 0; i < 100000; i++)
{
    tree.Insert(r.Next());
}

Console.WriteLine($"Depth {tree.GetMinMaxDepth()}");

var tree2 = new AvlTree<int>();
tree2.Insert(1);
tree2.Insert(2);
tree2.Insert(3);
tree2.Insert(4);
Console.WriteLine($"Depth {tree2.GetMinMaxDepth()}");
