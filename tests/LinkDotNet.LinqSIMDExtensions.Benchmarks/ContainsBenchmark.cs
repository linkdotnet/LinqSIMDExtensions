using BenchmarkDotNet.Attributes;

namespace LinkDotNet.LinqSIMDExtensions.Benchmarks;

public class ContainsBenchmark
{
    private readonly int[] _numbers = Enumerable.Range(0, 100_000).ToArray();

    [Benchmark(Baseline = true)]
    public bool LinqContains() => Enumerable.Contains(_numbers, 80000);

    [Benchmark]
    public bool LinqSIMDContains() => LinqSIMDExtensions.Contains(_numbers, 80000);

    [Benchmark]
    public bool SIMDLinqContains() => SimdLinq.SimdLinqExtensions.Contains(_numbers, 80000);
}
