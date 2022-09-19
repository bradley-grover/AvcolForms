using AvcolForms.Core;

namespace AvcolForms.Tests.Unit.Core;

/// <summary>
/// Tests the <see cref="SpanExtensions"/> class methods
/// </summary>
public class SpanExtensionsTests
{
    /// <summary>
    /// The span should not return true on any elements for this condition
    /// </summary>
    [Theory]
    [InlineData(new byte[] { 1 })]
    [InlineData(new byte[] { 1, 3, 4, 7 })]
    public void Span_Should_NotHaveAny(byte[] sequence)
    {
        Span<byte> s = sequence.AsSpan();

        Assert.False(s.Any(x => x == 5));
    }

    /// <summary>
    /// The span should throw on a null predicate
    /// </summary>
    [Fact]
    public void Should_ThrowOnNullPredicate()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Span<byte> span = Array.Empty<byte>();
            span.Any(null!);
        });
    }
}
