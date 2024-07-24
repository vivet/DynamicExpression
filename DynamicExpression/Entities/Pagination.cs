using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DynamicExpression.Entities;

/// <summary>
/// Pagination.
/// </summary>
public class Pagination
{
    /// <summary>
    /// Max Pagination.
    /// </summary>
    public const int MAX_PAGINATION = 25000;

    /// <summary>
    /// Number.
    /// </summary>
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public virtual int Number { get; set; } = 1;

    /// <summary>
    /// Count.
    /// </summary>
    [DefaultValue(25)]
    [Range(1, Pagination.MAX_PAGINATION)]
    public virtual int Count { get; set; } = 25;

    /// <summary>
    /// Skip.
    /// Skips a number of items.
    /// If set, then it Overrides <see cref="Number"/>.
    /// </summary>
    [Range(0, int.MaxValue)]
    public virtual int? Skip { get; set; }
}