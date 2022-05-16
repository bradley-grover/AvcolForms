namespace AvcolForms.Tests.Unit.Core.Files;

/// <summary>
/// Tests an overload of <see cref="IFileSaver.SaveAsync(Stream, string, CancellationToken)"/>
/// </summary>
public class SaveOnStreamTests
{
    private static readonly IFileSaver _fileSaver = new FileSaver();
    private readonly string _filePath = Path.Join(Directory.GetCurrentDirectory(), "/Assets/savedFile.png");

    /// <summary>
    /// The save should throw on an empty/null stream
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Save_Should_ThrowOnEmpty()
    {
        static async Task func() => await _fileSaver.SaveAsync(Stream.Null, "test.png");

        await Assert.ThrowsAsync<ArgumentException>(func);
    }

    /// <summary>
    /// The save should throw on the path argument being invalid
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task SaveShould_ThrowOnPath()
    {
        static async Task func() => await _fileSaver.SaveAsync(new MemoryStream(new byte[] { 1, 2, 3}), null!);
        await Assert.ThrowsAnyAsync<ArgumentNullException>(func);
    }

    /// <summary>
    /// Should save the file correctly
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Should_Save()
    {
        FileStream stream = File.OpenRead(_filePath);

        long length = stream.Length;

        string savePath = Path.Join(Directory.GetCurrentDirectory(), "/Output/savedImageStream.png");

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        await _fileSaver.SaveAsync(stream, savePath);

        using var fileStream = File.Open(savePath, FileMode.Open);

        Assert.True(File.Exists(savePath));
        Assert.Equal(length, fileStream.Length);
    }
}
