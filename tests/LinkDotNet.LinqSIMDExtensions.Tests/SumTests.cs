using Shouldly;
using Xunit;

namespace LinkDotNet.LinqSIMDExtensions.Tests;

public class SumTests
{
    [Fact]
    public void GivenSomeNumbers_WhenRetrievingTheSumInArrays_ThenTheCorrectSumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var sum = numbers.Sum();

        sum.ShouldBe(55);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingSumInLists_ThenTheCorrectSumIsReturned()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var sum = numbers.Sum();

        sum.ShouldBe(55);
    }

    [Fact]
    public void GivenNullList_WhenCallingSum_ThenArgumentNullExceptionIsThrown()
    {
        List<int> numbers = null;

        Assert.Throws<ArgumentNullException>(() => numbers.Sum());
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingSumInSpans_ThenTheCorrectSumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var sum = numbers.AsSpan().Sum();

        sum.ShouldBe(55);
    }
}