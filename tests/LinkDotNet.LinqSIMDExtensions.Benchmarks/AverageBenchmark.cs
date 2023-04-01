using BenchmarkDotNet.Attributes;

namespace LinkDotNet.LinqSIMDExtensions.Benchmarks;

public class AverageBenchmark
{
    private readonly float[] _numbers = Enumerable.Range(0, 1000).Select(f => (float)f).ToArray();

    [Benchmark(Baseline = true)]
    public float LinqAverage() => Enumerable.Average(_numbers);

    [Benchmark]
    public float LinqSIMDAverage() => LinqSIMDExtensions.Average(_numbers);
}
