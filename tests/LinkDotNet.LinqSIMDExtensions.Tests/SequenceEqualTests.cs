using Shouldly;
using Xunit;

namespace LinkDotNet.LinqSIMDExtensions.Tests;

public class SequenceEqualTests
{
    [Fact]
    public void GivenTwoSpans_WhenCheckingSequenceEquals_ThenTheCorrectResultIsReturned()
    {
        var first = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var second = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var result = MemoryExtensions.SequenceEqual(first.AsSpan(), second.AsSpan());

        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenTwoSpans_WhenCheckingSequenceEqualsWithDifferentLength_ThenTheCorrectResultIsReturned()
    {
        var first = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var second = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        var result = MemoryExtensions.SequenceEqual(first.AsSpan(), second.AsSpan());

        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenTwoSpans_WhenCheckingSequenceEqualWithDifferentValues_ThenTheCorrectResultIsReturned()
    {
        var first = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var second = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 };

        var result = MemoryExtensions.SequenceEqual(first.AsSpan(), second.AsSpan());

        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenTwoLists_WhenCheckingSequenceEqual_ThenTheCorrectResultIsReturned()
    {
        var first = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var second = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var result = first.SequenceEqual(second);

        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenTwoLists_WhenOneIsNull_ThenArgumentNullExceptionIsThrown()
    {
        List<int> first = null!;
        var second = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Action act = () => first.SequenceEqual(second);

        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenTwoArrays_WhenCheckingSequenceEqual_ThenTheCorrectResultIsReturned()
    {
        var first = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var second = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var result = first.SequenceEqual(second);

        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenTwoArrays_WhenCheckingSequenceEqualWhereOneIsNull_ThenExceptionIsThrown()
    {
        int[] first = null!;
        var second = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Action act = () => first.SequenceEqual(second);

        act.ShouldThrow<ArgumentNullException>();
    }
}
