using System.ComponentModel;
using DynamicExpression.Enums;
using DynamicExpression.Interfaces;

namespace DynamicExpression.Entities;

/// <inheritdoc />
public class Ordering : IOrdering
{
    /// <inheritdoc />
    [DefaultValue("Id")]
    public virtual string By { get; set; } = "Id";

    /// <inheritdoc />
    [DefaultValue(OrderingDirection.Asc)]
    public virtual OrderingDirection Direction { get; set; } = OrderingDirection.Asc;
}