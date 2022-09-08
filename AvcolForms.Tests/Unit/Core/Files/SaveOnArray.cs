namespace AvcolForms.Tests.Unit.Core.Files;

/// <summary>
/// Tests saving with the <see cref="Memory{T}"/> overload of <see cref="IFileSaver.SaveAsync(ReadOnlyMemory{byte}, string, CancellationToken)"/>
/// </summary>
public class SaveOnArrayTests
{
    private static readonly IFileSaver _fileSaver = new FileSaver();
    private readonly string _filePath = Path.Join(Directory.GetCurrentDirectory(), "/Assets/savedFile.png");

    /// <summary>
    /// Save should throw on an empty memory block
    /// </summary>
    /// <returns>A <see cref="Task"/></returns>
    [Fact]
    public async Task Save_Should_Throw_OnEmpty()
    {
        static async Task func() => await _fileSaver.SaveAsync(Array.Empty<byte>(), null!);

        await Assert.ThrowsAnyAsync<ArgumentException>(func);
    }

    /// <summary>
    /// Save should throw when trying to save to a bad path
    /// </summary>
    /// <returns>A <see cref="Task"/></returns>
    [Fact]
    public async Task Save_Should_Throw_OnPath()
    {
        static async Task func() => await _fileSaver.SaveAsync(new byte[] { 0, 1, 2 }, null!);
        await Assert.ThrowsAnyAsync<ArgumentNullException>(func);
    }

    /// <summary>
    /// Save should work as intended
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Should_Save()
    {
        FileStream stream = File.OpenRead(_filePath);

        long length = stream.Length;

        string savePath = Path.Join(Directory.GetCurrentDirectory(), "/Output/savedImage.png");

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        MemoryStream memoryStream = new();


        stream.CopyTo(memoryStream);

        Memory<byte> memory = memoryStream.ToArray();

        await _fileSaver.SaveAsync(memory, savePath);

        stream?.Dispose();

        try
        {
            using var fileStream = File.Open(savePath, FileMode.Open);

            Assert.True(File.Exists(savePath));
            Assert.Equal(length, fileStream.Length);
        }
        catch (IOException)
        {

        }
    }
}
