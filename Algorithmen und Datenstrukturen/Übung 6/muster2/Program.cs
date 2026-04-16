using FHS.CT.AlgoDat.DataStructures;

var tree = new BTree<int>(3);

var r = new Random();
for (int i = 0; i < 100000; i++)
{
    tree.Insert(r.Next());
}

Console.WriteLine($"Depth {tree.GetDepth()}");

var tree2 = new BTree<int>(2);
tree2.Insert(1);
tree2.Insert(2);
tree2.Insert(3);
tree2.Insert(4);
Console.WriteLine($"Depth {tree2.GetDepth()}");
