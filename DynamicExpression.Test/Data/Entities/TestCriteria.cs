using DynamicExpression.Interfaces;

namespace DynamicExpression.Test.Data.Entities
{
    public class TestCriteria : IQueryCriteria
    {
        public virtual CriteriaExpression GetExpression<T>()
            where T : class
        {
            var expression = new CriteriaExpression();

            return expression;
        }
    }
}