using System;
using System.Collections.Generic;
using System.Linq;
using DynamicExpression.Enums;

namespace DynamicExpression.Entities;

/// <summary>
/// Criteria.
/// </summary>
public class Criteria
{
    /// <summary>
    /// Value.
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Value2.
    /// </summary>
    public object Value2 { get; set; }

    /// <summary>
    /// Property.
    /// </summary>
    public string Property { get; set; }

    /// <summary>
    /// Logical Type.
    /// </summary>
    public LogicalType LogicalType { get; set; }

    /// <summary>
    /// Operation Type.
    /// </summary>
    public OperationType OperationType { get; set; }
}

/// <inheritdoc />
public class Criteria<TType> : Criteria
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="property">The property name.</param>
    /// <param name="operationType">The <see cref="OperationType"/>.</param>
    /// <param name="value">The value.</param>
    /// <param name="value2">the value2</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public Criteria(string property, OperationType operationType, TType value, TType value2, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        var type = typeof(TType);
        var isSupported = this.GetSupportedOperationTypes(type).Contains(operationType);

        if (!isSupported)
            throw new InvalidOperationException(operationType.ToString());

        this.Property = property;
        this.Value = value;
        this.Value2 = value2;
        this.LogicalType = logicalType;
        this.OperationType = operationType;
    }

    private IEnumerable<OperationType> GetSupportedOperationTypes(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        var operationTypes = new Dictionary<string, HashSet<Type>>
        {
            { "Text", new HashSet<Type> { typeof(string), typeof(char) } },
            { "Number", new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { "Boolean", new HashSet<Type> { typeof(bool) } },
            { "Date", new HashSet<Type> { typeof(DateTime), typeof(DateTimeOffset) } },
            { "Nullable", new HashSet<Type> { typeof(Nullable<>) } },
            { "Guid", new HashSet<Type> { typeof(Guid) } }
        };

        if (type.IsArray)
            return new[] { OperationType.In, OperationType.NotIn, OperationType.Contains, OperationType.NotContains };

        var operationType = type.IsEnum
            ? "Enum"
            : operationTypes.FirstOrDefault(x => x.Value.Any(y => y.Name == type.Name)).Key;

        switch (operationType)
        {
            case "Text":
                return new[]
                {
                    OperationType.Equal,
                    OperationType.NotEqual,
                    OperationType.StartsWith,
                    OperationType.EndsWith,
                    OperationType.IsEmpty,
                    OperationType.IsNotEmpty,
                    OperationType.IsNull,
                    OperationType.IsNotNull,
                    OperationType.IsNullOrWhiteSpace,
                    OperationType.IsNotNullOrWhiteSpace,
                    OperationType.IsEmpty,
                    OperationType.IsNotEmpty,
                    OperationType.In,
                    OperationType.NotIn,
                    OperationType.Contains,
                    OperationType.NotContains
                };

            case "Date":
            case "Number":
                return new[]
                {
                    OperationType.Equal,
                    OperationType.NotEqual,
                    OperationType.GreaterThan,
                    OperationType.GreaterThanOrEqual,
                    OperationType.LessThan,
                    OperationType.LessThanOrEqual,
                    OperationType.Between
                };

            case "Boolean":
                return new[]
                {
                    OperationType.Equal,
                    OperationType.NotEqual
                };

            case "Guid":
                return new[]
                {
                    OperationType.Equal,
                    OperationType.NotEqual
                };

            case "Enum":
                return new[]
                {
                    OperationType.Equal,
                    OperationType.NotEqual,
                    OperationType.In,
                    OperationType.NotIn,
                    OperationType.Contains,
                    OperationType.NotContains
                };

            case "Nullable":
                return new[]
                    {
                        OperationType.IsNull,
                        OperationType.IsNotNull
                    }
                    .Union(this.GetSupportedOperationTypes(Nullable.GetUnderlyingType(type)));

            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}