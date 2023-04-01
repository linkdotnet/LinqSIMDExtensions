using System.Numerics;

namespace LinkDotNet.LinqSIMDExtensions;

internal static class VectorHelper
{
    public static Vector<T> CreateWithValue<T>(T fillValue)
        where T : unmanaged
    {
        Span<T> vector = stackalloc T[Vector<T>.Count];
        vector.Fill(fillValue);
        return new Vector<T>(vector);
    }
}
