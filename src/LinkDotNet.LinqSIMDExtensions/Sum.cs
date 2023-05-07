using System.Numerics;
using System.Runtime.InteropServices;

namespace LinkDotNet.LinqSIMDExtensions;

public static partial class LinqSIMDExtensions
{
    /// <summary>
    /// Gets the sum of the elements in the list.
    /// </summary>
    public static T Sum<T>(this List<T> list)
        where T : unmanaged, INumberBase<T>
    {
        ArgumentNullException.ThrowIfNull(list);
        var span = CollectionsMarshal.AsSpan(list);

        return Sum(span);
    }

    /// <summary>
    /// Gets the sum of the elements in the array.
    /// </summary>
    public static T Sum<T>(this T[] array)
	    where T : unmanaged, INumberBase<T>
    {
        ArgumentNullException.ThrowIfNull(array);

        return Sum(array.AsSpan());
    }

    /// <summary>
    /// Gets the sum of the elements in the memory.
    /// </summary>
    public static T Sum<T>(this Memory<T> memory)
        where T : unmanaged, INumberBase<T>
        => Sum(memory.Span);

    /// <summary>
    /// Gets the sum of the elements in the span.
    /// </summary>
    public static T Sum<T>(this Span<T> span)
        where T : unmanaged, INumberBase<T>
    {
        var spanAsVectors = MemoryMarshal.Cast<T, Vector<T>>(span);
        var remainingElements = span.Length % Vector<T>.Count;
        var accVector = new Vector<T>();

        for (var i = 0; i < spanAsVectors.Length; i += 2)
        {
            accVector += spanAsVectors[i] + spanAsVectors[i + 1];
        }

        if (spanAsVectors.Length % 2 == 1)
        {
            accVector += spanAsVectors[^1];
        }

        if (remainingElements > 0)
        {
            Span<T> lastVectorElements = stackalloc T[Vector<T>.Count];
            span[^remainingElements..].CopyTo(lastVectorElements);
            accVector += new Vector<T>(lastVectorElements);
        }

        return Vector.Sum(accVector);
    }
}
