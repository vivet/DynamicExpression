using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicExpression.Test
{
    [TestClass]
    public class CriteriaBuilderTest
    {
        public class TestType
        {
            public virtual string Name { get; set; }
        }

        [TestMethod]
        public void GetExpressionWhenStartsWithTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.StartsWith("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.GetExpression<TestType>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso x.Name.Trim().ToLower().StartsWith(\"value\".Trim().ToLower()))", expression.Body.ToString());
        }

        [TestMethod]
        public void GetExpressionWhenEndsWithTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.EndsWith("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.GetExpression<TestType>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso x.Name.Trim().ToLower().EndsWith(\"value\".Trim().ToLower()))", expression.Body.ToString());
        }
    }
}
