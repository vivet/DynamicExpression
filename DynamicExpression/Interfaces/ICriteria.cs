using DynamicExpression.Enums;

namespace DynamicExpression.Interfaces
{
    /// <summary>
    /// Criteria interface.
    /// </summary>
    public interface ICriteria
    {
        /// <summary>
        /// Value.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Value 2.
        /// </summary>
        object Value2 { get; set; }

        /// <summary>
        /// Property.
        /// </summary>
        string Property { get; set; }

        /// <summary>
        /// Logical Type.
        /// </summary>
        LogicalType LogicalType { get; set; }

        /// <summary>
        /// Operation Type.
        /// </summary>
        OperationType OperationType { get; set; }
    }
}