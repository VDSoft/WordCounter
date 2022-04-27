// <copyright company="VDSoft" file="IFileReader.cs">
//    Copyright (C) VDSoft. All rights reserved. Confidential.
// </copyright>

namespace VDsoft.TextProcessor.BusinessLogic.Infrastructure;

/// <summary>
/// Defines a file reader to access the file system.
/// </summary>
public interface IFileReader
{
    /// <summary>
    /// Reads the content of a file asynchronous.
    /// </summary>
    /// <param name="filePath">The path of the file to read.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the asynchronous task.</param>
    /// <returns>A <see cref="Task"/> for the asynchronous operation, containing the content of the provided file.</returns>
    Task<string> ReadFileAsync(string filePath, CancellationToken cancellationToken);
}
