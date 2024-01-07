using System.Numerics;
using System.Runtime.InteropServices;

namespace LinkDotNet.LinqSIMDExtensions;

public static partial class LinqSIMDExtensions
{
    /// <summary>
    /// Retrieves the minimum value of the list.
    /// </summary>
    public static T Min<T>(this List<T> list)
        where T : unmanaged, IMinMaxValue<T>, INumber<T>
    {
        ArgumentNullException.ThrowIfNull(list);
        var span = CollectionsMarshal.AsSpan(list);

        return Min(span);
    }

    /// <summary>
    /// Retrieves the minimum value of the array.
    /// </summary>
    public static T Min<T>(this T[] array)
        where T : unmanaged, IMinMaxValue<T>, INumber<T>
    {
        ArgumentNullException.ThrowIfNull(array);

        return Min(array.AsSpan());
    }

    /// <summary>
    /// Retrieves the minimum value of the memory.
    /// </summary>
    /// <returns>The max as type T.</returns>
    public static T Min<T>(this Memory<T> memory)
        where T : unmanaged, IMinMaxValue<T>, INumber<T>
        => Min(memory.Span);

    /// <summary>
    /// Retrieves the minimum value of the span.
    /// </summary>
    public static T Min<T>(this Span<T> span)
        where T : unmanaged, IMinMaxValue<T>, INumber<T>
    {
        var spanAsVectors = MemoryMarshal.Cast<T, Vector<T>>(span);
        var minVector = VectorHelper.CreateWithValue(T.MaxValue);

        for (var i = 0; i < spanAsVectors.Length - 1; i += 2)
        {
            var iterationMin = Vector.Min(spanAsVectors[i], spanAsVectors[i + 1]);
            minVector = Vector.Min(minVector, iterationMin);
        }

        if (spanAsVectors.Length % 2 == 1)
        {
            minVector = Vector.Min(minVector, spanAsVectors[^1]);
        }

        var remainingElements = span.Length % Vector<T>.Count;
        if (remainingElements > 0)
        {
            Span<T> lastVectorElements = stackalloc T[Vector<T>.Count];
            lastVectorElements.Fill(T.MaxValue);
            span[^remainingElements..].CopyTo(lastVectorElements);
            minVector = Vector.Min(minVector, new Vector<T>(lastVectorElements));
        }

        var minValue = T.MaxValue;
        for (var i = 0; i < Vector<T>.Count; i++)
        {
            minValue = T.Min(minValue, minVector[i]);
        }

        return minValue;
    }
}
