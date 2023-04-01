using BenchmarkDotNet.Attributes;

namespace LinkDotNet.LinqSIMDExtensions.Benchmarks;

public class SequenceEqualBenchmark
{
    private readonly int[] _numbers1 = Enumerable.Range(0, 10_000).ToArray();
    private readonly int[] _numbers2 = Enumerable.Range(0, 10_000).ToArray();

    [Benchmark(Baseline = true)]
    public bool LinqSequenceEqual() => Enumerable.SequenceEqual(_numbers1, _numbers2);

    [Benchmark]
    public bool LinqSIMDSequenceEqual() => LinqSIMDExtensions.SequenceEqual(_numbers1, _numbers2);
}
