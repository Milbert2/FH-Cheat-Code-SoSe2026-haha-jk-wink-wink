using FHS.CT.AlgoDat.DataStructures;

// Thanks to Michelle Türtscher for the test cases :)
var searchTree = new Tree<int>();
searchTree.Root = new Tree<int>.Node(4);
searchTree.Root.Left = new Tree<int>.Node(2);
searchTree.Root.Right = new Tree<int>.Node(6);
searchTree.Root.Left.Left = new Tree<int>.Node(1);
searchTree.Root.Left.Right = new Tree<int>.Node(3);
searchTree.Root.Right.Left = new Tree<int>.Node(5);
searchTree.Root.Right.Right = new Tree<int>.Node(7);


Console.WriteLine($"Testing binary search tree: {AlgoDat.BSTChecker<int>.Check(searchTree)}");
    
var nonSearchTree = new Tree<int>();
nonSearchTree.Root = new Tree<int>.Node(7);
nonSearchTree.Root.Right = new Tree<int>.Node(8);
nonSearchTree.Root.Right.Right = new Tree<int>.Node(9);
nonSearchTree.Root.Right.Left = new Tree<int>.Node(4);

Console.WriteLine($"Testing NON binary search tree: {AlgoDat.BSTChecker<int>.Check(nonSearchTree)}");
