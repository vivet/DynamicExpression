using DynamicExpression.Entities;

namespace DynamicExpression.Interfaces;

/// <summary>
/// Query interface.
/// </summary>
public interface IQuery
{
    /// <summary>
    /// Order.
    /// </summary>
    Ordering Order { get; set; }

    /// <summary>
    /// Paging.
    /// </summary>
    Pagination Paging { get; set; }
}

/// <summary>
/// Query (generic) interface.
/// </summary>
/// <typeparam name="TCriteria">The type of <see cref="IQueryCriteria"/>.</typeparam>
public interface IQuery<TCriteria> : IQuery
    where TCriteria : IQueryCriteria
{
    /// <summary>
    /// Criteria.
    /// </summary>
    TCriteria Criteria { get; set; }
}