using System.ComponentModel;

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
    public virtual int Number { get; set; } = 1;

    /// <summary>
    /// Count.
    /// </summary>
    [DefaultValue(25)]
    public virtual int Count { get; set; } = 25;
}