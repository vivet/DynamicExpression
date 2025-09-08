using System;
using System.Collections.Generic;
using DynamicExpression.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetTopologySuite.Geometries;

namespace DynamicExpression.Test;

[TestClass]
public class CriteriaBuilderTest
{
    [TestMethod]
    public void BuildWhenEqualTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Name != null) AndAlso (x.Name == \"value\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenEqualWhenTimeSpanTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("Date", DateOnly.MaxValue);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual($"(x.Date == {DateOnly.MaxValue})", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenEqualWhenDateOnlyTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("Date", DateOnly.MaxValue);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual($"(x.Date == {DateOnly.MaxValue})", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenEqualWhenDateTimeTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("DateTime", DateTime.MaxValue);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual($"(x.DateTime == {DateTime.MaxValue})", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenEqualWhenDateTimeOffsetTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("DateTimeOffset", DateTimeOffset.MaxValue);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual($"(x.DateTimeOffset == {DateTimeOffset.MaxValue})", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenEqualAndEnumTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("Flags", FlagsEnum.One);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual($"((x.Id == {guid}) AndAlso ((x.IdNullable != null) AndAlso (x.IdNullable == {guid})))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenNotEqualTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.NotEqual("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual($"((x.Id != {guid}) AndAlso ((x.IdNullable == null) OrElse (x.IdNullable != {guid})))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenStartsWithTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.StartsWith("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Name != null) AndAlso x.Name.StartsWith(\"value\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenEndsWithTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.EndsWith("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Name != null) AndAlso x.Name.EndsWith(\"value\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenGreaterThanTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.GreaterThan("Age", 1);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Age > 1)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenGreaterThanOrEqualTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.GreaterThanOrEqual("Age", 1);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Age >= 1)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenLessThanTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.LessThan("Age", 1);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Age < 1)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenLessThanOrEqualTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.LessThanOrEqual("Age", 1);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Age <= 1)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenBetweenTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Between("Age", 1, 5);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Age >= 1) AndAlso (x.Age <= 5))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsNullTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsNull<string>("Name");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Name == null)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsNullWhenGuidNullableTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsNull<Guid?>("IdNullable");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.IdNullable == null)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsNotNullTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsNotNull<string>("Name");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Name != null)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsNotNullGuidNullableTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsNotNull<Guid?>("IdNullable");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.IdNullable != null)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsEmptyTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsEmpty<string>("Name");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Name == \"\")", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsNotEmptyTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsNotEmpty<string>("Name");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("(x.Name != \"\")", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsNullOrWhiteSpaceTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsNullOrWhiteSpace<string>("Name");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Name == null) OrElse (x.Name.Trim() == \"\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsNotNullOrWhiteSpaceTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.IsNotNullOrWhiteSpace<string>("Name");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Name != null) AndAlso (x.Name.Trim() != \"\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenInTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.In("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("x.Name.Contains(\"value\")", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenInAndEnumTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.In("Flags", FlagsEnum.One | FlagsEnum.Two);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenInAndArrayEnumTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.In("Flags", new[] { FlagsEnum.One, FlagsEnum.Two });

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("value(DynamicExpression.Test.CriteriaBuilderTest+FlagsEnum[]).Contains(x.Flags)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenInAndArrayTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.In("Name", new[] { "value", "value2" });

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("value(System.String[]).Contains(x.Name)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenNotInTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.NotIn("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("Not(x.Name.Contains(\"value\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenNotInAndEnumTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.NotIn("Flags", FlagsEnum.One | FlagsEnum.Two);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("Not(((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenNotInAndArrayTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.NotIn("Name", new[] { "value", "value2" });

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("Not(value(System.String[]).Contains(x.Name))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenContainsTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Contains("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("x.Name.Contains(\"value\")", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenContainsAndEnumTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Contains("Flags", FlagsEnum.One | FlagsEnum.Two);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenContainsAndIsArrayTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Contains("Name", new[] { "value", "value2" });

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("value(System.String[]).Contains(x.Name)", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenNotContainsTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.NotContains("Name", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("Not(x.Name.Contains(\"value\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenNotContainsAndEnumTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.NotContains("Flags", FlagsEnum.One | FlagsEnum.Two);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("Not(((Convert(x.Flags, Int32) | Convert(One, Two, Int32)) == Convert(One, Two, Int32)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenCoversTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.Covers(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.Covers(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenCrossesTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.Crosses(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.Crosses(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenTouchesTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.Touches(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.Touches(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenOverlapsTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.Overlaps(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.Overlaps(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenCoveredByTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.CoveredBy(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.CoveredBy(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenDisjointTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.Disjoint(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.Disjoint(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIntersectsTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.Intersects(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.Intersects(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenWithinTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.Within(nameof(Customer.Location), new Point(0, 0));

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.Within(POINT (0 0)))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenIsWithinDistanceTest()
    {
        var criteriaExpression = new CriteriaExpression();

        criteriaExpression.IsWithinDistance(nameof(Customer.Location), new Point(0, 0), 100);

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Location != null) AndAlso x.Location.IsWithinDistance(POINT (0 0), 100))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenReferenceTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("Payment.Id", "value");

        var expression = CriteriaBuilder.Build<Order>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("((x.Payment.Id != null) AndAlso (x.Payment.Id == \"value\"))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenReferenceCollectionTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("Orders[Payment.Id]", "value");

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("x.Orders.Any(i => ((i.Payment.Id != null) AndAlso (i.Payment.Id == \"value\")))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenRefernceAndReferenceCollectionTest()
    {
        var criteriaExpression = new CriteriaExpression();
        criteriaExpression.Equal("Customer.Orders[Payment.Id]", "value");

        var expression = CriteriaBuilder.Build<User>(criteriaExpression);

        Assert.IsNotNull(expression);
        Assert.IsNotNull(expression.Compile());
        Assert.AreEqual("x.Customer.Orders.Any(i => ((i.Payment.Id != null) AndAlso (i.Payment.Id == \"value\")))", expression.Body.ToString());
    }

    [TestMethod]
    public void BuildWhenMultipleCriteriaExpressionsTest()
    {
        var criteriaExpression1 = new CriteriaExpression();
        criteriaExpression1.Equal("Name", "value");

        var criteriaExpression2 = new CriteriaExpression();
        criteriaExpression2.Equal("Name", "value2", LogicalType.Or);
        criteriaExpression2.Equal("Name", "value3");

        var expression = CriteriaBuilder.Build<Customer>([criteriaExpression1, criteriaExpression2]);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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

        var expression = CriteriaBuilder.Build<Customer>(criteriaExpression);

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
    public class User
    {
        public virtual Customer Customer { get; set; }
    }
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public virtual Guid? IdNullable { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual FlagsEnum Flags { get; set; } = FlagsEnum.One | FlagsEnum.Two;
        public virtual IEnumerable<Order> Orders { get; set; }
        public virtual TimeOnly Time { get; set; }
        public virtual DateOnly Date { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual DateTimeOffset DateTimeOffset { get; set; }
        public virtual Point Location { get; set; }
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