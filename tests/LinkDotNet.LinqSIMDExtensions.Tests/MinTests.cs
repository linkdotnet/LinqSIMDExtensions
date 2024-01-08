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

        var min = numbers.Min();

        min.ShouldBe(1);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMinimumInSpan_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var min = numbers.AsSpan().Min();

        min.ShouldBe(1);
    }

    [Fact]
    public void GivenNullList_WhenRetrievingMinimum_ThenArgumentNullExceptionIsThrown()
    {
        List<int> numbers = null!;

        Assert.Throws<ArgumentNullException>(() => numbers.Min());
    }

    [Fact]
    public void GivenNullArray_WhenRetrievingMinimum_ThenArgumentNullExceptionIsThrown()
    {
        int[] numbers = null!;

        Assert.Throws<ArgumentNullException>(() => numbers.Min());
    }

    [Fact]
    public void GivenMemory_WhenRetrievingMinimum_ThenTheCorrectMinimumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var min = numbers.AsMemory().Min();

        min.ShouldBe(1);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(1, 2, 3, 4)]
    [InlineData(1, 2, 3, 4, 5, 6)]
    public void GivenUltraShortList_WhenGettingMinimum_ThenTheCorrectMinimumIsReturned(params int[] numbers)
    {
        var min = numbers.Min();

        min.ShouldBe(1);
    }

    [Fact]
    public void GivenLongerList_WhenGettingMinimum_ThenTheCorrectMinimumIsReturned()
    {
        var sequence = Enumerable.Range(0, 100).ToList();

        var min = sequence.Min();

        min.ShouldBe(0);
    }

    [Fact]
    public void ShouldThrowInvalidOperationException_WhenSequenceIsEmpty()
    {
        var sequence = new List<int>();

        Should.Throw<InvalidOperationException>(() => sequence.Min());
    }
}
