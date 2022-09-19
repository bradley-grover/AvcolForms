using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace AvcolForms.Core;

/// <summary>
/// Provides extensions for <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/>
/// </summary>
public static class SpanExtensions
{
    /// <summary>
    /// Determines whether any element in the span satisfies a condition
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the span</typeparam>
    /// <param name="source">The span itself</param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <returns>Whether any element satisfied the condition</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<TSource>(this Span<TSource> source, Func<TSource, bool> predicate) => Any((ReadOnlySpan<TSource>)source, predicate);

    /// <summary>
    /// Determines whether any element in the span satisfies a condition
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the span</typeparam>
    /// <param name="source">The span itself</param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <returns>Whether any element satisfied the condition</returns>
    [Pure]
    public static bool Any<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                return true;
            }
        }

        return false;
    }
}
