using Shouldly;
using Xunit;

namespace LinkDotNet.LinqSIMDExtensions.Tests;

public class MaxTests
{
    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMaximumInArrays_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var max = numbers.Max();

        max.ShouldBe(10);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMaximumInList_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var max = numbers.Max();

        max.ShouldBe(10);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMaximumInSpan_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var max = numbers.AsSpan().Max();

        max.ShouldBe(10);
    }

    [Fact]
    public void GivenNullList_WhenRetrievingMaximum_ThenArgumentNullExceptionIsThrown()
    {
        List<int> numbers = null;

        Action act = () => numbers.Max();
        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenNullArray_WhenRetrievingMaximum_ThenArgumentNullExceptionIsThrown()
    {
        int[] numbers = null;

        Action act = () => numbers.Max();
        act.ShouldThrow<ArgumentNullException>();
    }
}