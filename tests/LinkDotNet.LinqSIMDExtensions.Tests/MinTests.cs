using Shouldly;
using Xunit;

namespace LinkDotNet.LinqSIMDExtensions.Tests;

public class MinTests
{
    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMinimumInArrays_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var min = numbers.Min();

        min.ShouldBe(1);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMinimumInList_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var sum = numbers.Min();

        sum.ShouldBe(1);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMinimumInSpan_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var sum = numbers.AsSpan().Min();

        sum.ShouldBe(1);
    }

    [Fact]
    public void GivenNullList_WhenRetrievingMinimum_ThenArgumentNullExceptionIsThrown()
    {
        List<int> numbers = null;

        Assert.Throws<ArgumentNullException>(() => numbers.Min());
    }

    [Fact]
    public void GivenNullArray_WhenRetrievingMinimum_ThenArgumentNullExceptionIsThrown()
    {
        int[] numbers = null;

        Assert.Throws<ArgumentNullException>(() => numbers.Min());
    }
}