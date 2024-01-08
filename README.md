# LinqSIMDExtensions 

[![.NET](https://github.com/linkdotnet/LinqSIMDExtensions/actions/workflows/dotnet.yml/badge.svg)](https://github.com/linkdotnet/LinqSIMDExtensions/actions/workflows/dotnet.yml)
[![Nuget](https://img.shields.io/nuget/dt/LinkDotNet.LinqSIMDExtensions)](https://www.nuget.org/packages/LinkDotNet.LinqSIMDExtensions/)
[![GitHub tag](https://img.shields.io/github/v/tag/linkdotnet/LinqSIMDExtensions?include_prereleases&logo=github&style=flat-square)](https://github.com/linkdotnet/LinqSIMDExtensions/releases)

LinqSIMDExtensions is a high-performance library that combines the power of SIMD (Single Instruction, Multiple Data) and the elegance of LINQ (Language Integrated Query) syntax. SIMD uses features like AVX, SSE, NEON, etc. to process multiple data in parallel. The performance depends on the hardware and the data type. Some extensions like AVX-512 are only available on x86/x64 architectures.

LinqSIMDExtensions leverages the generic math features introduced in .NET 7 to provide a wide range of data type support.
With LinkDotNet.LinqSIMDExtensions, you can efficiently process large datasets in parallel, improving the performance of your applications.

Using this library for small datasets is not recommended as the overhead of the SIMD operations is not worth it.

## Features
 * Leverage SIMD operations for fast and parallel processing of data
 * Support for a wide range of data types (thanks to generic math)
 * LINQ syntax so you can almost use it as drop-in replacement
 * Currently supports: `Min`, `Max`, `Sum`, `SequenceEqual`, `Average`, `Contains`

## Installation
To install the package via NuGet, run the following command :
```no-class
dotnet add package LinkDotNet.LinqSIMDExtensions
```

## Usage
First you have to import the namespace:
```csharp
using LinkDotNet.LinqSIMDExtensions;
```

Then you can use the extension methods:
```csharp
List<int> numbers = GetNumbers();
var result = numbers.Min();
```

## Benchmark
You can head over to [benchmark section](tests/LinkDotNet.LinqSIMDExtensions.Benchmarks/) to see the setup. The following section will show you some results of different machines and architectures. Keep in mind to test against your specific use case with the hardware that will run it! SIMD can vary a lot depending on the hardware. Newer hardware tends to have better support for SIMD (if x86/x64 is used).

Here are the benchmarks for a M1 Pro. Keep in mind that the M1 (arm64) does not support all SIMD operations. So the performance is not as good as on a x64 machine.

```no-class
BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.2.1 (22D68) [Darwin 22.3.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.202
  [Host]     : .NET 7.0.4 (7.0.423.11508), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.4 (7.0.423.11508), Arm64 RyuJIT AdvSIMD


| Method          |        Mean |    Error |   StdDev | Ratio |
| --------------- | ----------: | -------: | -------: | ----: |
| LinqMin         |   176.93 ns | 0.783 ns | 0.732 ns |  1.00 |
| LinqSIMDMin     |    65.48 ns | 0.255 ns | 0.238 ns |  0.37 |
|                 |             |          |          |       |
| LinqAverage     | 1,058.29 ns | 5.354 ns | 5.008 ns |  1.00 |
| LinqSIMDAverage |    80.97 ns | 0.465 ns | 0.435 ns |  0.08 |
```

## Constraints
As we are using SIMD the following constraints apply:
 * The collection type has to be contiguous memory (e.g. `List<T>`, `T[]`, `Span<T>`). Types like `IEnumerable<T>` or `IReadOnlyList<T>` are not supported.
 * The underlying number has to implement specific interfaces (like `IMinMaxValue`) to be able to determine the min/max value of the type.
 * Some functions like `Average` can not return a more specific type than the input out of the box. This means that if you have a `List<int>` and call `Average` on it, the result will be a `int` and not an `double`. There are overloads that enable a more specific return type than the input.
 * There are no arithmetic checks. So it is prone to overflow or underflow.
 * `Contains` on `List<T>` has to be invoked like this: `LinqSIMDExtensions.Contains(list, value)`. This is due to the fact that the `Contains` method is already defined on `List<T>` and the compiler can not resolve the correct method.
 * The underlying data type has to be supported by the SIMD instruction. For example `Complex` is not supported as it is not a primitive type.
