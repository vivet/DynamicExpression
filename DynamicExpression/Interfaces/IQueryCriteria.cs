namespace DynamicExpression.Interfaces
{
    /// <summary>
    /// Query Criteria interface.
    /// </summary>
    public interface IQueryCriteria
    {
        /// <summary>
        /// Gets the <see cref="CriteriaExpression"/>.
        /// </summary>
        /// <returns>The <see cref="CriteriaExpression"/></returns>
        CriteriaExpression GetExpression();
    }
}