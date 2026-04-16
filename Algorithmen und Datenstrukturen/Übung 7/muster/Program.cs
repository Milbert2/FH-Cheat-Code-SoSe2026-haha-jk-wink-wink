using FHS.CT.AlgoDat.DataStructures;

// test cases from Andrea Bucher
{
    // Korrekter Min-Heap
    var validHeap = new Tree<int>();
    validHeap.Root = new Tree<int>.Node(2)
    {
        Left = new Tree<int>.Node(3),
        Right = new Tree<int>.Node(4)
    };
    validHeap.Root.Left.Left = new Tree<int>.Node(5);
    validHeap.Root.Left.Right = new Tree<int>.Node(6);
    Console.WriteLine("Korrekter Heap: " + validHeap.CheckHeapProperties(validHeap.Root)); // True

    // Falscher Heap (Eltern > Kind)
    var invalidHeap = new Tree<int>();
    invalidHeap.Root = new Tree<int>.Node(5)
    {
        Left = new Tree<int>.Node(3),
        Right = new Tree<int>.Node(4)
    };
    invalidHeap.Root.Left.Left = new Tree<int>.Node(2); // 3 > 2 → Verletzung
    Console.WriteLine("Ungültiger Heap: " + invalidHeap.CheckHeapProperties(invalidHeap.Root)); // False

    // Unvollständiger Baum
    var incompleteTree = new Tree<int>();
    incompleteTree.Root = new Tree<int>.Node(1)
    {
        Left = new Tree<int>.Node(2),
        Right = new Tree<int>.Node(3)
    };
    incompleteTree.Root.Left.Right = new Tree<int>.Node(5); // Lücke im linken Kind
    Console.WriteLine("Unvollständiger Baum: " + incompleteTree.CheckHeapProperties(incompleteTree.Root)); // False
}

// test cases from Andreas Kamm
{

    Tree<int> tree = new Tree<int>();

    Tree<int> tree1 = new Tree<int>(); // ist ein Heap
    tree1.Root = new Tree<int>.Node(1);
    tree1.Root.Left = new Tree<int>.Node(6);
    tree1.Root.Right = new Tree<int>.Node(2);

    Tree<int> tree2 = new Tree<int>(); // kein Heap weil minheapify nicht passt
    tree2.Root = new Tree<int>.Node(5);
    tree2.Root.Left = new Tree<int>.Node(7);
    tree2.Root.Right = new Tree<int>.Node(2);

    Tree<int> tree3 = new Tree<int>(); // kein Heap weil nicht vollständig
    tree3.Root = new Tree<int>.Node(8);
    tree3.Root.Left = new Tree<int>.Node(11);
    tree3.Root.Right = new Tree<int>.Node(10);
    tree3.Root.Left.Right = new Tree<int>.Node(21);
    // tree3.Root.Left.Left = new Tree<int>.Node(10);

    Console.WriteLine(tree1.CheckHeapProperties(tree1.Root!));
    Console.WriteLine(tree2.CheckHeapProperties(tree2.Root!));
    Console.WriteLine(tree3.CheckHeapProperties(tree3.Root!));
}