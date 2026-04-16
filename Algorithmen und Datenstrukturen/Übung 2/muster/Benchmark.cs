using AlgoDat;
using BenchmarkDotNet.Attributes;

public class Benchmark
{
    [Params(4000, 8000, 16000, 32000, 64000, 128000, 256000, 512000)]
    public int N;

    [Params(0, 8, 16, 32, 64)]
    public int Cutoff;

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
    public void AlgoDatSort()
    {
        var sorter = new AlgoDatSort<int>();
        sorter.Cutoff = Cutoff;

        sorter.Sort(_numbers!);
    }
}