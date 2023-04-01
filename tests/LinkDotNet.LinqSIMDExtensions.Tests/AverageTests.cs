using Shouldly;
using Xunit;

namespace LinkDotNet.LinqSIMDExtensions.Tests;

public class AverageTests
{
    [Fact]
    public void GivenSomeIntegers_WhenAverage_ThenAverageIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var average = numbers.AsSpan().Average();

        average.ShouldBe(5);
    }

    [Fact]
    public void GivenSomeFloats_WhenAverage_ThenAverageIsReturned()
    {
        var numbers = new[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f };

        var average = numbers.AsSpan().Average();

        average.ShouldBe(5.5f);
    }

    [Fact]
    public void GivenSomeDoublesInAList_WhenAverage_ThenAverageIsReturned()
    {
        var numbers = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };

        var average = numbers.Average();

        average.ShouldBe(5.5);
    }

    [Fact]
    public void GivenNullList_WhenAverage_ThenArgumentNullExceptionIsThrown()
    {
        List<int> numbers = null!;

        Should.Throw<ArgumentNullException>(() => numbers.Average());
    }

    [Fact]
    public void GivenSomeIntegersInArray_WhenAverage_ThenAverageIsReturned()
    {
        var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var average = numbers.Average();

        average.ShouldBe(5);
    }

    [Fact]
    public void GivenNullArray_WhenAverage_ThenArgumentNullExceptionIsThrown()
    {
        int[] numbers = null!;

        Should.Throw<ArgumentNullException>(() => numbers.Average());
    }
}
