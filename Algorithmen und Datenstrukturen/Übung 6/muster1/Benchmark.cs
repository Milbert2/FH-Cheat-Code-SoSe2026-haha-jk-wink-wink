using BenchmarkDotNet.Attributes;

using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

public class Benchmark
{

    private int[] GetInput(int n)
    {
        var numbers = new int[n];
        var r = new Random();

        for (int i = 0; i < n; i++)
        {
            numbers[i] = r.Next();
        }

        return numbers;
    }

    [Benchmark]
    public void BinarySearchTree()
    {
        var input = GetInput(1000000);
        var r = new Random();
        var tree = new BinarySearchTree<int>();
        foreach (int n in input)
        {
            tree.Insert(n);
        }

        for (int i = 0; i < 10000; i++)
        {
            var rPos = r.Next(0, input.Length);
            tree.Search(input[rPos]);
        }
    }

    [Benchmark]
    public void AvlTree()
    {
        var input = GetInput(1000000);
        var r = new Random();
        var tree = new AvlTree<int>();
        foreach (int n in input)
        {
            tree.Insert(n);
        }

        for (int i = 0; i < 10000; i++)
        {
            var rPos = r.Next(0, input.Length);
            tree.Search(input[rPos]);
        }
    }

    [Benchmark]
    [Arguments(3)]
    [Arguments(4)]
    [Arguments(8)]
    [Arguments(16)]
    [Arguments(32)]
    [Arguments(64)]
    [Arguments(128)]
    public void BTree(int t)
    {
        var input = GetInput(1000000);
        var r = new Random();
        var tree = new BTree<int>(t);
        foreach (int n in input)
        {
            tree.Insert(n);
        }

        for (int i = 0; i < 10000; i++)
        {
            var rPos = r.Next(0, input.Length);
            tree.Search(input[rPos]);
        }
    }
}