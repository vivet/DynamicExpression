using System.Linq;
using DynamicExpression.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicExpression.Test
{
    [TestClass]
    public class CriteriaExpressionTest
    {
        [TestMethod]
        public void ByTest()
        {
            var expression = new CriteriaExpression();
            expression.By("name", OperationType.Contains, "value");

            Assert.AreEqual(1, expression.Criterias.Count);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("name", criteria.Property);
            Assert.AreEqual("value", criteria.Value);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.Contains, criteria.OperationType);
        }

        [TestMethod]
        public void StartsWithTest()
        {
            var expression = new CriteriaExpression();
            expression.StartsWith("name", "value");

            Assert.AreEqual(1, expression.Criterias.Count);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("name", criteria.Property);
            Assert.AreEqual("value", criteria.Value);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.StartsWith, criteria.OperationType);
        }
    }
}