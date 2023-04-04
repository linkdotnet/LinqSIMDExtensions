# LinqSIMDExtensions 

[![.NET](https://github.com/linkdotnet/LinqSIMDExtensions/actions/workflows/dotnet.yml/badge.svg)](https://github.com/linkdotnet/LinqSIMDExtensions/actions/workflows/dotnet.yml)
[![Nuget](https://img.shields.io/nuget/dt/LinkDotNet.LinqSIMDExtensions)](https://www.nuget.org/packages/LinkDotNet.LinqSIMDExtensions/)
[![GitHub tag](https://img.shields.io/github/v/tag/linkdotnet/LinqSIMDExtensions?include_prereleases&logo=github&style=flat-square)](https://github.com/linkdotnet/LinqSIMDExtensions/releases)

LinqSIMDExtensions is a high-performance library that combines the power of SIMD (Single Instruction, Multiple Data) and the elegance of LINQ (Language Integrated Query) syntax.
It leverages the generic math features introduced in .NET 7 to provide a wide range of data type support.
With LinkDotNet.LinqSIMDExtensions, you can easily process large datasets in parallel, improving the performance of your applications.

It is not recommend to use this library for small datasets as the overhead of the SIMD operations is not worth it.

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
You can head over to [benchmark section](tests/LinkDotNet.LinqSIMDExtensions.Benchmarks/) to see the setup.

```no-class
BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.2.1 (22D68) [Darwin 22.3.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.202
  [Host]     : .NET 7.0.4 (7.0.423.11508), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.4 (7.0.423.11508), Arm64 RyuJIT AdvSIMD


| Method          |       Mean |   Error |  StdDev | Ratio |
| --------------- | ---------: | ------: | ------: | ----: |
| LinqSUM         |   328.6 ns | 0.77 ns | 0.72 ns |  1.00 |
| LinqSIMDSUM     |   118.9 ns | 0.57 ns | 0.51 ns |  0.36 |
|                 |            |         |         |       |
| LinqAverage     | 1,050.7 ns | 1.04 ns | 0.98 ns |  1.00 |
| LinqSIMDAverage |   178.6 ns | 0.28 ns | 0.25 ns |  0.17 |

```

## Constraints
As we are using SIMD the following constraints apply:
 * The collection type has to be contiguous memory (e.g. `List<T>`, `T[]`, `Span<T>`). Types like `IEnumerable<T>` or `IReadOnlyList<T>` are not supported.
 * The underlying number has to implement specific interfaces (like `IMinMaxValue`) to be able to determine the min/max value of the type.
 * Some functions like `Average` can not return a more specific type than the input. This means that if you have a `List<int>` and call `Average` on it, the result will be a `int` and not an `double`.
 * There are no arithmetic checks. So it is prune to overflow or underflow.
 * `Contains` on `List<T>` has to be invoked like this: `LinqSIMDExtensions.Contains(list, value)`. This is due to the fact that the `Contains` method is already defined on `List<T>` and the compiler can not resolve the correct method.
