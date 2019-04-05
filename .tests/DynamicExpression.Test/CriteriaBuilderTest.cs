using System;
using System.Collections.Generic;
using DynamicExpression.Enums;
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("(Convert(x.Flags, Int32) == Convert(One, Int32))", expression.Body.ToString());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("((x.Name != null) AndAlso (x.Name.Trim() != \"\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenInTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.In("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("x.Name.Contains(\"value\")", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenInAndEnumTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.In("Flags", FlagsEnum.One | FlagsEnum.Two);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenInAndArrayTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.In("Name", new[] { "value", "value2" });

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("value(System.String[]).Contains(x.Name)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotInTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotIn("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("Not(x.Name.Contains(\"value\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotInAndEnumTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotIn("Flags", FlagsEnum.One | FlagsEnum.Two);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("Not(((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32)))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotInAndArrayTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotIn("Name", new[] { "value", "value2" });

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("Not(value(System.String[]).Contains(x.Name))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenContainsTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Contains("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32))", expression.Body.ToString());
        }
                
        [TestMethod]
        public void BuildWhenContainsAndIsArrayTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Contains("Name", new[] { "value", "value2" });

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("value(System.String[]).Contains(x.Name)", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenNotContainsTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.NotContains("Name", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
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
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("Not(((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32)))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenReferenceTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Payment.Id", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Order>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("((x.Payment.Id != null) AndAlso (x.Payment.Id == \"value\"))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenReferenceCollectionTest()
        {
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Orders[Payment.Id]", "value");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("x.Orders.Any(i => ((i.Payment.Id != null) AndAlso (i.Payment.Id == \"value\")))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenMultipleCriteriaExpressionsTest()
        {
            var criteriaExpression1 = new CriteriaExpression();
            criteriaExpression1.Equal("Name", "value");

            var criteriaExpression2 = new CriteriaExpression();
            criteriaExpression2.Equal("Name", "value2", LogicalType.Or);
            criteriaExpression2.Equal("Name", "value3");

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(new[] { criteriaExpression1, criteriaExpression2 });

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual("(((x.Name != null) AndAlso (x.Name == \"value\")) AndAlso (((x.Name != null) AndAlso (x.Name == \"value2\")) OrElse ((x.Name != null) AndAlso (x.Name == \"value3\"))))", expression.Body.ToString());
        }
        
        [TestMethod]
        public void BuildWhenPropertyIsNullableAndValueIsNullableTest()
        {
            Guid? guid = Guid.NewGuid();
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("IdNullable", guid);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual($"((x.IdNullable != null) AndAlso (x.IdNullable == {guid}))", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenPropertyIsNullableAndValueIsNotNullableTest()
        {
            var guid = Guid.NewGuid();
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("IdNullable", guid);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual($"((x.IdNullable != null) AndAlso (x.IdNullable == {guid}))", expression.Body.ToString());
        }
        
        [TestMethod]
        public void BuildWhenPropertyIsNotNullableAndValueIsNullableTest()
        {
            Guid? guid = Guid.NewGuid();
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Id", guid);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Console.WriteLine(expression.Body.ToString());

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual($"(x.Id == {guid})", expression.Body.ToString());
        }

        [TestMethod]
        public void BuildWhenPropertyIsNotNullableAndValueIsNotNullableTest()
        {
            var guid = Guid.NewGuid();
            var criteriaExpression = new CriteriaExpression();
            criteriaExpression.Equal("Id", guid);

            var builder = new CriteriaBuilder();
            var expression = builder.Build<Customer>(criteriaExpression);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Compile());
            Assert.AreEqual($"(x.Id == {guid})", expression.Body.ToString());
        }


        [Flags]
        public enum FlagsEnum
        {
            One = 1 << 0,
            Two = 1 << 1
        }

        public class Customer
        {
            public virtual Guid Id { get; set; }
            public virtual Guid? IdNullable { get; set; }
            public virtual string Name { get; set; }
            public virtual int Age { get; set; }
            public virtual FlagsEnum Flags { get; set; } = FlagsEnum.One | FlagsEnum.Two;
            public virtual IEnumerable<Order> Orders { get; set; }
        }

        public class Payment
        {
            public virtual string Id { get; set; }
        }

        public class Order
        {
            public virtual Payment Payment { get; set; }
        }
    }
}
