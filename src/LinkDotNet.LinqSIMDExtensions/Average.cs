using System.Numerics;
using System.Runtime.InteropServices;

namespace LinkDotNet.LinqSIMDExtensions;

public static partial class LinqSIMDExtensions
{
    /// <summary>
    /// Returns the average of the elements in the list.
    /// </summary>
    /// <typeparam name="T">Any number-like object that is able to use the division operation.</typeparam>
    /// <returns>Returns the average in the same given type as the input.</returns>
    /// <remarks>
    /// As the input as well as the output is of type <typeparamref name="T"/> the result is maybe not rounded mathematically correct.
    /// Given two integers 1 and 2 the average is 1.5 but the result is 1.
    /// </remarks>
    public static T Average<T>(this List<T> list)
        where T : unmanaged, INumberBase<T>, IDivisionOperators<T, T, T>
    {
        ArgumentNullException.ThrowIfNull(list);

        return Average(CollectionsMarshal.AsSpan(list));
    }

    /// <summary>
    /// Returns the average of the elements in the array.
    /// </summary>
    /// <typeparam name="T">Any number-like object that is able to use the division operation.</typeparam>
    /// <returns>Returns the average in the same given type as the input.</returns>
    /// <remarks>
    /// As the input as well as the output is of type <typeparamref name="T"/> the result is maybe not rounded mathematically correct.
    /// Given two integers 1 and 2 the average is 1.5 but the result is 1.
    /// </remarks>
    public static T Average<T>(this T[] array)
        where T : unmanaged, INumberBase<T>, IDivisionOperators<T, T, T>
    {
        ArgumentNullException.ThrowIfNull(array);

        return Average(array.AsSpan());
    }

    /// <summary>
    /// Returns the average of the elements in the span.
    /// </summary>
    /// <typeparam name="T">Any number-like object that is able to use the division operation.</typeparam>
    /// <returns>Returns the average in the same given type as the input.</returns>
    /// <remarks>
    /// As the input as well as the output is of type <typeparamref name="T"/> the result is maybe not rounded mathematically correct.
    /// Given two integers 1 and 2 the average is 1.5 but the result is 1.
    /// </remarks>
    public static T Average<T>(this Span<T> span)
        where T : unmanaged, INumberBase<T>, IDivisionOperators<T, T, T>
    {
        var length = T.CreateChecked(span.Length);
        var divisionOperators = span.Sum() / length;
        return divisionOperators;
    }
}
