using BenchmarkDotNet.Attributes;

using FHS.CT.AlgoDat.Algorithms;

public class Benchmark
{
    [Params(4000, 8000, 16000, 32000, 64000, 128000, 256000)]
    public int N;

    private int[]? _numbers;

    [IterationSetup]
    public void GenerateNumbers()
    {
        var r = new Random();
        _numbers = new int[N];
        for (int i = 0; i < N; i++)
        {
            _numbers[i] = r.Next();
        }
    }

    [Benchmark]
    public void SelectionSort()
    {
        SelectionSort<int>.Sort(_numbers!);
    }
}