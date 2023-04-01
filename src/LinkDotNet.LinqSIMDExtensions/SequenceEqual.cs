using System.Numerics;
using System.Runtime.InteropServices;

namespace LinkDotNet.LinqSIMDExtensions;

public static partial class LinqSIMDExtensions
{
    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
    /// </summary>
    public static bool SequenceEqual<T>(this List<T> list, List<T> other)
        where T : unmanaged, IEquatable<T>
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(other);
        var span = CollectionsMarshal.AsSpan(list);
        var otherSpan = CollectionsMarshal.AsSpan(other);

        return SequenceEqual(span, otherSpan);
    }

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
    /// </summary>
    public static bool SequenceEqual<T>(this T[] array, T[] other)
        where T : unmanaged, IEquatable<T>
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(other);
        var span = array.AsSpan();
        var otherSpan = other.AsSpan();

        return SequenceEqual(span, otherSpan);
    }

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
    /// </summary>
    public static bool SequenceEqual<T>(this Span<T> span, Span<T> other)
        where T : unmanaged, IEquatable<T>
    {
        if (span.Length != other.Length)
        {
            return false;
        }

        var spanAsVectors = MemoryMarshal.Cast<T, Vector<T>>(span);
        var otherAsVectors = MemoryMarshal.Cast<T, Vector<T>>(other);

        for (var i = 0; i < spanAsVectors.Length; i++)
        {
            if (spanAsVectors[i] != otherAsVectors[i])
            {
                return false;
            }
        }

        var remainingElements = span.Length % Vector<T>.Count;
        if (remainingElements > 0)
        {
            Span<T> lastVectorElements = stackalloc T[Vector<T>.Count];
            span[^remainingElements..].CopyTo(lastVectorElements);
            Span<T> otherLastVectorElements = stackalloc T[Vector<T>.Count];
            other[^remainingElements..].CopyTo(otherLastVectorElements);
            if (new Vector<T>(lastVectorElements) != new Vector<T>(otherLastVectorElements))
            {
                return false;
            }
        }

        return true;
    }
}
