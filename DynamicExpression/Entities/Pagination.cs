using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DynamicExpression.Entities;

/// <summary>
/// 
/// </summary>
public class Pagination
{
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
    [Range(1, 1000)]
    public virtual int Count { get; set; } = 25;
}