using BenchmarkDotNet.Attributes;

namespace LinkDotNet.LinqSIMDExtensions.Benchmarks;

public class MinBenchmark
{
    private readonly int[] _numbers = Enumerable.Range(0, 1000).ToArray();

    [Benchmark(Baseline = true)]
    public int LinqSum() => Enumerable.Min(_numbers);

    [Benchmark]
    public int LinqSIMDSum() => LinqSIMDExtensions.Min(_numbers);
}
