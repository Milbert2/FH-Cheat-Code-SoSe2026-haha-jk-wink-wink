using FHS.CT.AlgoDat.DataStructures;

Random random = new Random();
BinarySearchTree<int> bst = new BinarySearchTree<int>();

for (int i = 0; i < 30; i++)
{
    bst.Insert(random.Next(100));
}

for (int k = 0; k < 30; k++)
{
    BinarySearchTree<int>.Node? n = bst.FindKthMinimum(k + 1);
    if (n == null)
    {
        Console.WriteLine("Whoops, this should not happen");
    }
    else
    {
        Console.WriteLine($"{k + 1}th smallest number is {n.Key}");
    }
}