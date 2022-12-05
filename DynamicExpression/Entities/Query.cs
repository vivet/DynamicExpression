using DynamicExpression.Interfaces;

namespace DynamicExpression.Entities;

/// <inheritdoc />
public class Query : IQuery
{
    /// <inheritdoc />
    public virtual Ordering Order { get; set; } = new();

    /// <inheritdoc />
    public virtual Pagination Paging { get; set; } = new();
}

/// <inheritdoc cref="IQuery{TCriteria}"/>
public class Query<TCriteria> : Query, IQuery<TCriteria>
    where TCriteria : IQueryCriteria, new()
{
    /// <inheritdoc />
    public virtual TCriteria Criteria { get; set; } = new();
}