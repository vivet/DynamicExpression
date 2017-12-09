using DynamicExpression.Interfaces;

namespace DynamicExpression
{
    /// <inheritdoc />
    public class Query : IQuery
    {
        /// <inheritdoc />
        public virtual IOrdering Order { get; set; }

        /// <inheritdoc />
        public virtual IPagination Paging { get; set; }
    }

    /// <inheritdoc cref="IQuery{TCriteria}"/>
    public class Query<TCriteria> : Query, IQuery<TCriteria>
        where TCriteria : IQueryCriteria
    {
        /// <inheritdoc />
        public virtual TCriteria Criteria { get; set; }
    }
}