using BenchmarkDotNet.Attributes;
using LinkDotNet.LinqSIMDExtensions;

namespace LinkDotNet.LinqSIMDExtensions.Benchmarks;

public class SumBenchmark
{
    private readonly int[] _numbers = Enumerable.Range(0, 1000).ToArray();

    [Benchmark(Baseline = true)]
    public int LinqSum() => Enumerable.Sum(_numbers);

    [Benchmark]
    public int LinqSIMDSum() => LinqSIMDExtensions.Sum(_numbers);
}
