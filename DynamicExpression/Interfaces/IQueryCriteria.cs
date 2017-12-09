using System.Linq.Expressions;

namespace DynamicExpression.Interfaces
{
    /// <summary>
    /// Query Criteria.
    /// </summary>
    public interface IQueryCriteria
    {
        /// <summary>
        /// Gets the <see cref="Expression{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The <see cref="CriteriaExpression"/></returns>
        CriteriaExpression GetExpression<T>()
            where T : class;
    }
}