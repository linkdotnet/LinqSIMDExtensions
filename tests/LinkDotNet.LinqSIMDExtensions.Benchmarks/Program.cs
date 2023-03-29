// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using LinkDotNet.LinqSIMDExtensions;

BenchmarkRunner.Run<SumBenchmark>();

public class SumBenchmark
{
    private readonly int[] _numbers = Enumerable.Range(0, 1000).ToArray();

    [Benchmark(Baseline = true)]
    public int LinqSUM() => Enumerable.Sum(_numbers);

    [Benchmark]
    public int LinqSIMDSUM() => LinqSIMDExtensions.Sum(_numbers);
}