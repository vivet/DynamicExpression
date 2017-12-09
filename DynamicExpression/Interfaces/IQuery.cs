namespace DynamicExpression.Interfaces
{
    /// <summary>
    /// Query.
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Order.
        /// </summary>
        IOrdering Order { get; set; }

        /// <summary>
        /// Paging.
        /// </summary>
        IPagination Paging { get; set; }
    }

    /// <summary>
    /// Query (generic).
    /// </summary>
    /// <typeparam name="TCriteria">The type of <see cref="IQuery"/>.</typeparam>
    public interface IQuery<TCriteria> : IQuery
        where TCriteria : IQueryCriteria
    {
        /// <summary>
        /// Query.
        /// </summary>
        TCriteria Criteria { get; set; }
    }
}