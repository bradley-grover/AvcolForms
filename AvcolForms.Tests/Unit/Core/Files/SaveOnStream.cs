using AvcolForms.Core.FileSaving;

namespace AvcolForms.Tests.Unit.Core.Files;

/// <summary>
/// Tests an overload of <see cref="IFileSaver.SaveAsync(Stream, string, CancellationToken)"/>
/// </summary>
public class SaveOnStreamTests
{
    private static readonly IFileSaver _fileSaver = new FileSaver();
    private readonly string _filePath = Path.Join(Directory.GetCurrentDirectory(), "/Assets/savedFile.png");



}
