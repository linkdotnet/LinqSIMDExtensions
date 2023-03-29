using System.Numerics;
using System.Runtime.InteropServices;

namespace LinkDotNet.LinqSIMDExtensions;

public static partial class LinqSIMDExtensions
{
    /// <summary>
    /// Retrieves the maximum value of the list.
    /// </summary>
    /// <returns>The max as type T.</returns>
    public static T Max<T>(this List<T> list)
        where T : unmanaged, IMinMaxValue<T>, INumber<T>
    {
        ArgumentNullException.ThrowIfNull(list);
        var span = CollectionsMarshal.AsSpan(list);

        return Max(span);
    }

    /// <summary>
    /// Retrieves the maximum value of the array.
    /// </summary>
    /// <returns>The max as type T.</returns>
    public static T Max<T>(this T[] array)
        where T : unmanaged, IMinMaxValue<T>, INumber<T>
    {
        ArgumentNullException.ThrowIfNull(array);

        return Max(array.AsSpan());
    }

    /// <summary>
    /// Retrieves the maximum value of the span.
    /// </summary>
    /// <returns>The max as type T.</returns>
    public static T Max<T>(this Span<T> span)
        where T : unmanaged, IMinMaxValue<T>, INumber<T>
    {
        var spanAsVectors = MemoryMarshal.Cast<T, Vector<T>>(span);

        Span<T> vector = stackalloc T[Vector<T>.Count];
        vector.Fill(T.MinValue);
        var minVector = new Vector<T>(vector);

        foreach (var spanAsVector in spanAsVectors)
        {
            minVector = Vector.Max(spanAsVector, minVector);
        }

        var remainingElements = spanAsVectors.Length % Vector<T>.Count;
        if (remainingElements > 0)
        {
            Span<T> lastVectorElements = stackalloc T[Vector<T>.Count];
            lastVectorElements.Fill(T.MinValue);
            span[^remainingElements..].CopyTo(lastVectorElements);
            minVector = Vector.Max(minVector, new Vector<T>(lastVectorElements));
        }

        var minValue = T.MinValue;
        for (var i = 0; i < Vector<T>.Count; i++)
        {
            minValue = T.Max(minValue, minVector[i]);
        }

        return minValue;
    }
}
