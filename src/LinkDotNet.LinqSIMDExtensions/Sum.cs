using System.IO.Compression;
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
    /// Gets the sum of the elements in the span.
    /// </summary>
    public static T Sum<T>(this Span<T> span)
        where T : unmanaged, INumberBase<T>
    {
        var spanAsVectors = MemoryMarshal.Cast<T, Vector<T>>(span);
        var accVector = VectorHelper.CreateWithValue(T.Zero);

        foreach (var spanAsVector in spanAsVectors)
        {
            accVector = Vector.Add(spanAsVector, accVector);
        }

        var remainingElements = spanAsVectors.Length % Vector<T>.Count;
        if (remainingElements > 0)
        {
            Span<T> lastVectorElements = stackalloc T[Vector<T>.Count];
            span[^remainingElements..].CopyTo(lastVectorElements);
            accVector = Vector.Add(accVector, new Vector<T>(lastVectorElements));
        }

        var oneVector = VectorHelper.CreateWithValue(T.One);
        return Vector.Dot(accVector, oneVector);
    }
}
