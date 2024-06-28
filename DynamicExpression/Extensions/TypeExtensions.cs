using System;
using System.Collections;

namespace DynamicExpression.Extensions;

/// <summary>
/// Type Extensions.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Is Array Or Enumerable.
    /// </summary>
    /// <param name="type">The <see cref="Type"/>.</param>
    /// <returns>Whether the .</returns>
    public static bool IsArrayOrEnumerable(this Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        return type.GetInterface(nameof(IEnumerable)) != null && type != typeof(string);
    }
}