using System.ComponentModel;
using DynamicExpression.Enums;

namespace DynamicExpression.Entities;

/// <summary>
/// Ordering.
/// </summary>
public class Ordering
{
    /// <summary>
    /// By,
    /// </summary>
    [DefaultValue("Id")]
    public virtual string By { get; set; } = "Id";

    /// <summary>
    /// By,
    /// </summary>
    public virtual string ThenBy { get; set; }

    /// <summary>
    /// Direction.
    /// </summary>
    [DefaultValue(OrderingDirection.Asc)]
    public virtual OrderingDirection Direction { get; set; } = OrderingDirection.Asc;
}