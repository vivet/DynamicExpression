using DynamicExpression.Interfaces;

namespace DynamicExpression.Entities
{
    /// <inheritdoc />
    public class Query : IQuery
    {
        /// <inheritdoc />
        public virtual IOrdering Order { get; set; } = new Ordering();

        /// <inheritdoc />
        public virtual IPagination Paging { get; set; } = new Pagination();
    }

    /// <inheritdoc cref="IQuery{TCriteria}"/>
    public class Query<TCriteria> : Query, IQuery<TCriteria>
        where TCriteria : IQueryCriteria, new()
    {
        /// <inheritdoc />
        public virtual TCriteria Criteria { get; set; } = new TCriteria();
    }
}