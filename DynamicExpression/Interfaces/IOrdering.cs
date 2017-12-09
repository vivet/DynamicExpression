using DynamicExpression.Enums;

namespace DynamicExpression.Interfaces
{
    /// <summary>
    /// Ordering.
    /// </summary>
    public interface IOrdering
    {
        /// <summary>
        /// By.
        /// </summary>
        string By { get; set; }

        /// <summary>
        /// Direction.
        /// </summary>
        OrderingDirection Direction { get; set; }
    }
}