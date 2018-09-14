using System.Linq;
using DynamicExpression.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicExpression.Test
{
    [TestClass]
    public class CriteriaExpressionTest
    {
        [TestMethod]
        public void ConstructorWhenEqualTest()
        {
            var expression = new CriteriaExpression();
            expression.Equal("name","value");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("name", criteria.Property);
            Assert.AreEqual("value", criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.Equal, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenEqualWhenNullTest()
        {
            var expression = new CriteriaExpression();
            expression.Equal("name", (string)null);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("name", criteria.Property);
            Assert.IsNull(criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.IsNull, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenStartsWithTest()
        {
            var expression = new CriteriaExpression();
            expression.StartsWith("name", "value");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("name", criteria.Property);
            Assert.AreEqual("value", criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.StartsWith, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenEndWithTest()
        {
            var expression = new CriteriaExpression();
            expression.EndsWith("name", "value");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("name", criteria.Property);
            Assert.AreEqual("value", criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.EndsWith, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenGreaterThanTest()
        {
            var expression = new CriteriaExpression();
            expression.GreaterThan("Age", 1);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Age", criteria.Property);
            Assert.AreEqual(1, criteria.Value);
            Assert.AreEqual(0, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.GreaterThan, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenGreaterThanOrEqualTest()
        {
            var expression = new CriteriaExpression();
            expression.GreaterThanOrEqual("Age", 1);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Age", criteria.Property);
            Assert.AreEqual(1, criteria.Value);
            Assert.AreEqual(0, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.GreaterThanOrEqualTo, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenLessThanTest()
        {
            var expression = new CriteriaExpression();
            expression.LessThan("Age", 1);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Age", criteria.Property);
            Assert.AreEqual(1, criteria.Value);
            Assert.AreEqual(0, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.LessThan, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenLessThanOrEqualTest()
        {
            var expression = new CriteriaExpression();
            expression.LessThanOrEqual("Age", 1);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Age", criteria.Property);
            Assert.AreEqual(1, criteria.Value);
            Assert.AreEqual(0, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.LessThanOrEqualTo, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenBetweenTest()
        {
            var expression = new CriteriaExpression();
            expression.Between("Age", 1, 5);

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Age", criteria.Property);
            Assert.AreEqual(1, criteria.Value);
            Assert.AreEqual(5, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.Between, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenIsNullTest()
        {
            var expression = new CriteriaExpression();
            expression.IsNull<string>("Name");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual(null, criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.IsNull, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenIsNotNullTest()
        {
            var expression = new CriteriaExpression();
            expression.IsNotNull<string>("Name");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual(null, criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.IsNotNull, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenIsEmptyTest()
        {
            var expression = new CriteriaExpression();
            expression.IsEmpty<string>("Name");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual(null, criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.IsEmpty, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenIsNotEmptyTest()
        {
            var expression = new CriteriaExpression();
            expression.IsNotEmpty<string>("Name");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual(null, criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.IsNotEmpty, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenIsNullOrWhiteSpaceTest()
        {
            var expression = new CriteriaExpression();
            expression.IsNullOrWhiteSpace<string>("Name");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual(null, criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.IsNullOrWhiteSpace, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenIsNotNullOrWhiteSpaceTest()
        {
            var expression = new CriteriaExpression();
            expression.IsNotNullOrWhiteSpace<string>("Name");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual(null, criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.IsNotNullOrWhiteSpace, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenNotContainsTest()
        {
            var expression = new CriteriaExpression();
            expression.NotContains("Name", "value");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual("value", criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.NotContains, criteria.OperationType);
        }

        [TestMethod]
        public void ConstructorWhenContainsTest()
        {
            var expression = new CriteriaExpression();
            expression.Contains("Name", "value");

            var criteria = expression.Criterias.FirstOrDefault();
            Assert.IsNotNull(criteria);
            Assert.AreEqual("Name", criteria.Property);
            Assert.AreEqual("value", criteria.Value);
            Assert.AreEqual(null, criteria.Value2);
            Assert.AreEqual(LogicalType.And, criteria.LogicalType);
            Assert.AreEqual(OperationType.Contains, criteria.OperationType);
        }
    }
}