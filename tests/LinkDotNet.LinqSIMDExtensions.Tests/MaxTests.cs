using Shouldly;
using Xunit;

namespace LinkDotNet.LinqSIMDExtensions.Tests;

public class MaxTests
{
    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMaximumInArrays_ThenTheCorrectMaximumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var max = numbers.Max();

        max.ShouldBe(10);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMaximumInList_ThenTheCorrectMaximumIsReturned()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var max = numbers.Max();

        max.ShouldBe(10);
    }

    [Fact]
    public void GivenSomeNumbers_WhenRetrievingMaximumInSpan_ThenTheCorrectMaximumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var max = numbers.AsSpan().Max();

        max.ShouldBe(10);
    }

    [Fact]
    public void GivenNullList_WhenRetrievingMaximum_ThenArgumentNullExceptionIsThrown()
    {
        List<int> numbers = null!;

        Action act = () => numbers.Max();
        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenNullArray_WhenRetrievingMaximum_ThenArgumentNullExceptionIsThrown()
    {
        int[] numbers = null!;

        Action act = () => numbers.Max();
        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenMemory_WhenRetrievingMaximum_ThenTheCorrectMaximumIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var max = numbers.AsMemory().Max();

        max.ShouldBe(10);
    }

    [Theory]
    [InlineData(6, 5)]
    [InlineData(6, 5, 4, 3)]
    [InlineData(1, 2, 3, 4, 5, 6)]
    public void GivenUltraShortList_WhenGettingMinimum_ThenTheCorrectMinimumIsReturned(params int[] numbers)
    {
        var max = numbers.Max();

        max.ShouldBe(6);
    }

    [Fact]
    public void GivenLongerList_WhenGettingMaximum_ThenTheCorrectMaximumIsReturned()
    {
        var sequence = Enumerable.Range(0, 21).Select(i => 20 - i).ToList();

        var max = sequence.Max();

        max.ShouldBe(20);
    }

    [Fact]
    public void ShouldThrowInvalidOperationException_WhenSequenceIsEmpty()
    {
        var sequence = new List<int>();

        Should.Throw<InvalidOperationException>(() => sequence.Max());
    }
}
