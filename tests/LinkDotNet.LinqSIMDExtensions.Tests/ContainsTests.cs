using Shouldly;
using Xunit;

namespace LinkDotNet.LinqSIMDExtensions.Tests;

public class ContainsTests
{
    [Fact]
    public void GivenSomeNumbers_WhenCheckingIfSpanContainsNumber_ThenTheCorrectResultIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var contains = numbers.AsSpan().Contains(5);

        contains.ShouldBeTrue();
    }

    [Fact]
    public void GiveSomeNumbersAtTheEnd_WhenCheckingIfSpanContainsNumber_ThenTheCorrectResultIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var contains = numbers.AsSpan().Contains(10);

        contains.ShouldBeTrue();
    }

    [Fact]
    public void GivenSomeNumbers_WhenCheckingIfListContainsNumber_ThenTheCorrectResultIsReturned()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var contains = LinqSIMDExtensions.Contains(numbers, 5);

        contains.ShouldBeTrue();
    }

    [Fact]
    public void GivenNullList_WhenCheckingIfListContainsNumber_ThenArgumentNullExceptionIsThrown()
    {
        List<int> numbers = null!;

        Action act = () => LinqSIMDExtensions.Contains(numbers, 5 );

        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenSomeNumbers_WhenCheckingIfArrayContainsNumber_ThenTheCorrectResultIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var contains = numbers.Contains(5);

        contains.ShouldBeTrue();
    }

    [Fact]
    public void GivenNullArray_WhenCheckingIfArrayContainsNumber_ThenArgumentNullExceptionIsThrown()
    {
        int[] numbers = null!;

        Action act = () => numbers.Contains(5);

        act.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void GivenSomeNumbers_WhenCheckingIfMemoryContainsNumber_ThenTheCorrectResultIsReturned()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var contains = numbers.AsMemory().Contains(5);

        contains.ShouldBeTrue();
    }
}
