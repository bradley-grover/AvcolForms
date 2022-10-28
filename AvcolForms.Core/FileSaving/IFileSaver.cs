/*
 * Licensed under the MIT License
 * Copyright (c) 2022 Bradley Grover
 */

using Microsoft.AspNetCore.Http;

namespace AvcolForms.Core.FileSaving;

/// <summary>
/// Abstraction to save a file, each path string should have an extension method to save it under
/// </summary>
public interface IFileSaver
{
    /// <summary>
    /// Saves the data to the specified path
    /// </summary>
    /// <param name="data"></param>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task SaveAsync(ReadOnlyMemory<byte> data, string path, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves a stream of data to the file path
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task SaveAsync(Stream stream, string path, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves an <see cref="IFormFile"/> to the file path
    /// </summary>
    /// <param name="file"></param>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task SaveAsync(IFormFile file, string path, CancellationToken cancellationToken = default);
}
