using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicExpression.Test
{
    [TestClass]
    public class CriteriaBuilderTest
    {
        [TestMethod]
        public void BuildWhenEqualTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso (x.Name == \"value\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenEqualAndEnumTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Flags", FlagsEnum.One);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((Convert(x.Flags, Int64) & Convert(One, Int64)) == Convert(One, Int64))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenEqualAndGuidTest()
        {
            var criteriaExpression = new CriteriaExpression();

            var guid = Guid.NewGuid();
            criteriaExpression.Equal("Id", guid);
            criteriaExpression.Equal("IdNullable", guid);
  
            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual($"((x.Id == {guid}) AndAlso ((x.IdNullable != null) AndAlso (x.IdNullable == {guid})))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenEqualAndGuidNullableTest()
        {
            var criteriaExpression = new CriteriaExpression();

            Guid? guid = Guid.NewGuid();
            criteriaExpression.Equal("Id", guid);
            criteriaExpression.Equal("IdNullable", guid);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual($"((x.Id == {guid}) AndAlso ((x.IdNullable != null) AndAlso (x.IdNullable == {guid})))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotEqualTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotEqual("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name == null) OrElse (x.Name != \"value\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotEqualAndGuidTest()
        {
            var guid = Guid.NewGuid();
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotEqual("Id", guid);
            criteriaExpression.NotEqual("IdNullable", guid);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual($"((x.Id != {guid}) AndAlso ((x.IdNullable == null) OrElse (x.IdNullable != {guid})))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotEqualAndGuidNullableTest()
        {
            Guid? guid = Guid.NewGuid();
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotEqual("Id", guid);
            criteriaExpression.NotEqual("IdNullable", guid);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual($"((x.Id != {guid}) AndAlso ((x.IdNullable == null) OrElse (x.IdNullable != {guid})))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenStartsWithTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.StartsWith("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso x.Name.StartsWith(\"value\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenEndsWithTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.EndsWith("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso x.Name.EndsWith(\"value\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenGreaterThanTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.GreaterThan("Age", 1);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Age > 1)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenGreaterThanOrEqualTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.GreaterThanOrEqual("Age", 1);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Age >= 1)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenLessThanTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.LessThan("Age", 1);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Age < 1)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenLessThanOrEqualTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.LessThanOrEqual("Age", 1);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Age <= 1)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenBetweenTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Between("Age", 1, 5);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Age >= 1) AndAlso (x.Age <= 5))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsNullTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsNull<string>("Name");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Name == null)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsNullWhenGuidNullableTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsNull<Guid?>("IdNullable");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.IdNullable == null)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsNotNullTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsNotNull<string>("Name");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Name != null)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsNotNullGuidNullableTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsNotNull<Guid?>("IdNullable");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.IdNullable != null)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsEmptyTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsEmpty<string>("Name");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Name == \"\")", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsNotEmptyTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsNotEmpty<string>("Name");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("(x.Name != \"\")", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsNullOrWhiteSpaceTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsNullOrWhiteSpace<string>("Name");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name == null) OrElse (x.Name.Trim() == \"\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenIsNotNullOrWhiteSpaceTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.IsNotNullOrWhiteSpace<string>("Name");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((x.Name != null) AndAlso (x.Name.Trim() != \"\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenContainsTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Contains("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("x.Name.Contains(\"value\")", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenContainsAndEnumTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Contains("Flags", FlagsEnum.One | FlagsEnum.Two);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("((Convert(x.Flags, Int64) | Convert(One, Two, Int64)) == Convert(One, Two, Int64))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotContainsTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotContains("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("Not(x.Name.Contains(\"value\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotContainsAndEnumTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotContains("Flags", FlagsEnum.One | FlagsEnum.Two);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("Not(((Convert(x.Flags, Int64) | Convert(One, Two, Int64)) == Convert(One, Two, Int64)))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenCollectionTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Orders[Payment.Id]", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.AreEqual("x.Orders.Any(i => ((i.Payment.Id != null) AndAlso (i.Payment.Id == \"value\")))", expression.Body.ToString());
        }

        [Flags]
        private enum FlagsEnum : long
        {
            One = 1 << 0,
            Two = 1 << 1
        }

        private class Payment
        {
            public virtual string Id { get; set; }
        }

        private class Order
        {
            public virtual Payment Payment { get; set; }
        }

        private class Customer
        {
            public virtual Guid Id { get; set; }
            public virtual Guid? IdNullable { get; set; }
            public virtual string Name { get; set; }
            public virtual int Age { get; set; }
            public virtual FlagsEnum Flags { get; set; } = FlagsEnum.One | FlagsEnum.Two;
            public virtual IEnumerable<Order> Orders { get; set; }
        }
    }
}
