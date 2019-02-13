using System.Collections.Generic;

namespace DynamicExpression.Interfaces
{
    /// <summary>
    /// Query Criteria interface.
    /// </summary>
    public interface IQueryCriteria
    {
        /// <summary>
        /// Gets the collection of <see cref="CriteriaExpression"/>'s.
        /// </summary>
        /// <returns>The <see cref="CriteriaExpression"/>'s.</returns>
        IList<CriteriaExpression> GetExpressions();
    }
}