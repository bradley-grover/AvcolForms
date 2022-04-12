using System.Reflection;
using AvcolForms.Core.FileSaving;

namespace AvcolForms.Tests.Unit.Core;

/// <summary>
/// Unit tests for an implementation of <see cref="IFileSaver"/>
/// </summary>
public class FileSavingTests
{
    private static readonly IFileSaver _fileSaver = new FileSaver();
    private readonly string _filePath = Path.Join(Directory.GetCurrentDirectory(), "/Assets/savedFile.png");

    /// <summary>
    /// Checks if the file saver validates the arguments
    /// </summary>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    [Fact]
    public async Task MemorySave_ShouldThrowOnEmpty()
    {
        static async Task func() => await _fileSaver.SaveAsync(Array.Empty<byte>(), null!);

        await Assert.ThrowsAnyAsync<ArgumentException>(func);
    }

    /// <summary>
    /// Checks if the file saver validates the path being null
    /// </summary>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    [Fact]
    public async Task MemorySave_ShowThrowOnPath()
    {
        static async Task func() => await _fileSaver.SaveAsync(new byte[] { 0, 1, 2 }, null!);
        await Assert.ThrowsAnyAsync<ArgumentNullException>(func);
    }

    /// <summary>
    /// Should save the image correctly
    /// </summary>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    [Fact]
    public async Task MemorySave_ShouldWork()
    {
        FileStream stream = File.OpenRead(_filePath);

        long length = stream.Length;

        string savePath = Path.Join(Directory.GetCurrentDirectory(), "/Output/savedImage.png");

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        await _fileSaver.SaveAsync(stream, savePath);

        stream?.Dispose();

        using var fileStream = File.Open(savePath, FileMode.Open);

        Assert.True(File.Exists(savePath));
        Assert.Equal(length, fileStream.Length);
    }
}
