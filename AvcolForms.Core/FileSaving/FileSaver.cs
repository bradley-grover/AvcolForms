/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using Microsoft.AspNetCore.Http;

namespace AvcolForms.Core.FileSaving;

/// <summary>
/// Saves files to the specified paths
/// </summary>
public sealed class FileSaver : IFileSaver
{
    /// <inheritdoc></inheritdoc>
    public async Task SaveAsync(ReadOnlyMemory<byte> data, string path, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(path, nameof(path));

        if (data.IsEmpty)
        {
            throw new ArgumentException("There should be some bytes in the data passed", nameof(data));
        }
        
        string? directory = Path.GetDirectoryName(path);
        
        if (directory is null)
        {
            throw new ArgumentException("Couldn't get directory from path", nameof(path));
        }

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(directory);
        }

        using var fileStream = File.Create(path);

        await fileStream.WriteAsync(data, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc></inheritdoc>
    public async Task SaveAsync(Stream stream, string path, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(path, nameof(path));

        if (stream == Stream.Null)
        {
            throw new ArgumentException("There should be a non null stream passed", nameof(stream));
        }

        if (stream.Length == 0)
        {
            throw new ArgumentException("There should be some bytes in the data passed", nameof(stream));
        }

        string? directory = Path.GetDirectoryName(path);

        if (directory is null)
        {
            throw new ArgumentException("Couldn't get directory from path", nameof(path));
        }

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(directory);
        }

        using var fileStream = File.Create(path);

        await stream.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc></inheritdoc>
    public async Task SaveAsync(IFormFile file, string path, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(path, nameof(path));

        if (file.Length == 0)
        {
            throw new ArgumentException("There should be some bytes in the data passed", nameof(file));
        }

        string? directory = Path.GetDirectoryName(path);

        if (directory is null)
        {
            throw new ArgumentException("Couldn't get directory from path", nameof(path));
        }

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(directory);
        }

        using var fileStream = File.Create(path);

        await file.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
    }
}
