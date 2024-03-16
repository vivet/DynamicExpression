using System;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace DynamicExpression.ModelBinders.Extensions;

/// <summary>
/// Stream Extensions.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Reads all bytes in the <see cref="Stream"/> and returns the content as string.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The content as string.</returns>
    public static async Task<string> ReadAllAsync(this Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        using var streamReader = new StreamReader(stream);

        return await streamReader
            .ReadToEndAsync(cancellationToken);
    }
}