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
    /// Returns the average of the elements in the list and returns the result in the given type <typeparamref name="TReturn"/>.
    /// </summary>
    /// <typeparam name="T">The input type of the list.</typeparam>
    /// <typeparam name="TReturn">The return type that is used for the calculation of the average (division).</typeparam>
    public static TReturn Average<T, TReturn>(this List<T> list)
        where T : unmanaged, INumberBase<T>
        where TReturn : unmanaged, INumberBase<TReturn>, IDivisionOperators<TReturn, TReturn, TReturn>
    {
        ArgumentNullException.ThrowIfNull(list);

        return Average<T, TReturn>(CollectionsMarshal.AsSpan(list));
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
    /// Returns the average of the elements in the list and returns the result in the given type <typeparamref name="TReturn"/>.
    /// </summary>
    /// <typeparam name="T">The input type of the list.</typeparam>
    /// <typeparam name="TReturn">The return type that is used for the calculation of the average (division).</typeparam>
    public static TReturn Average<T, TReturn>(this T[] array)
        where T : unmanaged, INumberBase<T>
        where TReturn : unmanaged, INumberBase<TReturn>, IDivisionOperators<TReturn, TReturn, TReturn>
    {
        ArgumentNullException.ThrowIfNull(array);

        return Average<T, TReturn>(array.AsSpan());
    }

    /// <summary>
    /// Returns the average of the elements in the memory.
    /// </summary>
    /// <typeparam name="T">Any number-like object that is able to use the division operation.</typeparam>
    /// <returns>Returns the average in the same given type as the input.</returns>
    /// <remarks>
    /// As the input as well as the output is of type <typeparamref name="T"/> the result is maybe not rounded mathematically correct.
    /// Given two integers 1 and 2 the average is 1.5 but the result is 1.
    /// </remarks>
    public static T Average<T>(this Memory<T> memory)
        where T : unmanaged, INumberBase<T>, IDivisionOperators<T, T, T>
        => Average(memory.Span);

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

    /// <summary>
    /// Returns the average of the elements in the list and returns the result in the given type <typeparamref name="TReturn"/>.
    /// </summary>
    /// <typeparam name="T">The input type of the list.</typeparam>
    /// <typeparam name="TReturn">The return type that is used for the calculation of the average (division).</typeparam>
    public static TReturn Average<T, TReturn>(this Span<T> span)
        where T : unmanaged, INumberBase<T>
        where TReturn : unmanaged, INumberBase<TReturn>, IDivisionOperators<TReturn, TReturn, TReturn>
    {
        var length = TReturn.CreateChecked(span.Length);
        var divisionOperators = TReturn.CreateChecked(span.Sum()) / length;
        return divisionOperators;
    }
}
