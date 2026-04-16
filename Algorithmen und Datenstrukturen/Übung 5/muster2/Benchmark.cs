using BenchmarkDotNet.Attributes;

using FHS.CT.AlgoDat.Algorithms;
using FHS.CT.AlgoDat.DataStructures;

public class Benchmark
{
    private int[]? _numbers;

    [IterationSetup(Targets = [nameof(BstRandom), nameof(AvlRandom)])]
    public void GenerateNumbers()
    {
        var r = new Random();
        _numbers = new int[100000];
        for (int i = 0; i < _numbers.Length; i++)
        {
            _numbers[i] = r.Next(2000);
        }
    }

    [IterationSetup(Targets = [nameof(BstSorted), nameof(AvlSorted)])]
    public void GenerateSortedNumbers()
    {
        _numbers = new int[100000];
        for (int i = 0; i < _numbers.Length; i++)
        {
            _numbers[i] = i;
        }
    }

    [Benchmark]
    public void BstRandom()
    {
        var tree = new BinarySearchTree<int>();
        foreach (int n in _numbers!)
        {
            tree.Insert(n);
        }

        var r = new Random();
        for (int i = 0; i < 10000; i++)
        {
            tree.Search(r.Next(2000));
        }
    }

    [Benchmark]
    public void AvlRandom()
    {
        var tree = new AvlTree<int>();
        foreach (int n in _numbers!)
        {
            tree.Insert(n);
        }

        var r = new Random();
        for (int i = 0; i < 10000; i++)
        {
            tree.Search(r.Next(2000));
        }
    }

        [Benchmark]
    public void BstSorted()
    {
        var tree = new BinarySearchTree<int>();
        foreach (int n in _numbers!)
        {
            tree.Insert(n);
        }

        var r = new Random();
        for (int i = 0; i < 10000; i++)
        {
            tree.Search(r.Next(2000));
        }
    }

    [Benchmark]
    public void AvlSorted()
    {
        var tree = new AvlTree<int>();
        foreach (int n in _numbers!)
        {
            tree.Insert(n);
        }

        var r = new Random();
        for (int i = 0; i < 10000; i++)
        {
            tree.Search(r.Next(2000));
        }
    }
}