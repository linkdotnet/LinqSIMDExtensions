using System.Numerics;
using System.Runtime.InteropServices;

namespace LinkDotNet.LinqSIMDExtensions;

public static partial class LinqSIMDExtensions
{
    /// <summary>
    /// Determines whether the list contains the specified value.
    /// </summary>
    public static bool Contains<T>(this List<T> list, T value)
        where T : unmanaged, INumberBase<T>
    {
        ArgumentNullException.ThrowIfNull(list);
        var span = CollectionsMarshal.AsSpan(list);

        return Contains(span, value);
    }

    /// <summary>
    /// Determines whether the array contains the specified value.
    /// </summary>
    public static bool Contains<T>(this T[] array, T value)
        where T : unmanaged, INumberBase<T>
    {
        ArgumentNullException.ThrowIfNull(array);

        return Contains(array.AsSpan(), value);
    }

    /// <summary>
    /// Determines whether the memory contains the specified value.
    /// </summary>
    public static bool Contains<T>(this Memory<T> memory, T value)
        where T : unmanaged, INumberBase<T>
        => Contains(memory.Span, value);

    /// <summary>
    /// Determines whether the span contains the specified value.
    /// </summary>
    public static bool Contains<T>(this Span<T> span, T value)
        where T : unmanaged, INumberBase<T>
    {
        var spanAsVectors = MemoryMarshal.Cast<T, Vector<T>>(span);
        var valueVector = VectorHelper.CreateWithValue(value);

        foreach (var spanAsVector in spanAsVectors)
        {
            if (Vector.EqualsAny(spanAsVector, valueVector))
            {
                return true;
            }
        }

        var remainingElements = span.Length % Vector<T>.Count;
        return remainingElements > 0 && ContainsSequential(span[^remainingElements..], value);
    }

    private static bool ContainsSequential<T>(Span<T> span, T value)
        where T : unmanaged, INumberBase<T>
    {
        foreach (var elem in span)
        {
            if (!elem.Equals(value))
            {
                continue;
            }

            return true;
        }

        return false;
    }
}
