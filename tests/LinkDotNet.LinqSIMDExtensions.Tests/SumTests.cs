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
        List<int> numbers = null!;

        Action act = () => numbers.Sum();
        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenNullArray_WhenCallingSum_ThenArgumentNullExceptionIsThrown()
    {
        int[] numbers = null!;

        Action act = () => numbers.Sum();
        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingSumInSpans_ThenTheCorrectSumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var sum = numbers.AsSpan().Sum();

        sum.ShouldBe(55);
    }

    [Fact]
    public void GivenMemory_WhenRetrievingSum_ThenTheCorrectSumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var sum = numbers.AsMemory().Sum();

        sum.ShouldBe(55);
    }

    [Fact]
    public void GivenEmptyList_WhenRetrievingSum_ThenZeroIsReturned()
    {
        var numbers = new List<int>();

        var sum = numbers.Sum();

        sum.ShouldBe(0);
    }
}
