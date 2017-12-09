using DynamicExpression.Enums;
using DynamicExpression.Interfaces;

namespace DynamicExpression.Entities
{
    /// <inheritdoc />
    public class Ordering : IOrdering
    {
        /// <inheritdoc />
        public virtual string By { get; set; } = "Id";

        /// <inheritdoc />
        public virtual OrderingDirection Direction { get; set; } = OrderingDirection.Asc;
    }
}