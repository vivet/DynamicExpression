using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicExpression.Test
{
    [TestClass]
    public class CriteriaBuilderTest
    {
        [Flags]
        public enum FlagsEnum : long
        {
            None = 0,
            One = 1 << 0,
            Two = 1 << 1,
            More = 1 << 2
        }

        public class Payment
        {
            public virtual string Id { get; set; }
        }

        public class Order
        {
            public virtual Payment Payment { get; set; }
        }

        public class Customer
        {
            public virtual string Name { get; set; }
            public virtual FlagsEnum Flags { get; set; } = FlagsEnum.One | FlagsEnum.Two;
            public virtual IEnumerable<Order> Orders { get; set; }
        }

        [TestMethod]
        public void GetExpressionWhenStartsWithTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.StartsWith("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.GetExpression<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso x.Name.Trim().ToLower().StartsWith(\"value\".Trim().ToLower()))", expression.Body.ToString());
        }

        [TestMethod]
        public void GetExpressionWhenEndsWithTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.EndsWith("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.GetExpression<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso x.Name.Trim().ToLower().EndsWith(\"value\".Trim().ToLower()))", expression.Body.ToString());
        }

        [TestMethod]
        public void GetExpressionWhenCollectionTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Orders[Payment.Id]", "abc");

            var builder = new CriteriaBuilder();
            var expression = builder.GetExpression<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("x.Orders.Any(i => ((i.Payment != null) AndAlso ((i.Payment.Id != null) AndAlso (i.Payment.Id.Trim().ToLower() == \"abc\".Trim().ToLower()))))", expression.Body.ToString());
        }

        [TestMethod]
        public void GetExpressionWhenFlagsEnumTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Flags", FlagsEnum.One);

            var builder = new CriteriaBuilder();
            var expression = builder.GetExpression<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((Convert(x.Flags, Int64) & Convert(One, Int64)) == Convert(One, Int64))", expression.Body.ToString());
        }
    }
}
