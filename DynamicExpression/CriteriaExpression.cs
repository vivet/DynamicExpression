using System;
using System.Collections.Generic;
using DynamicExpression.Entities;
using DynamicExpression.Enums;
using NetTopologySuite.Geometries;

namespace DynamicExpression;

/// <summary>
/// Criteria Expression.
/// </summary>
public class CriteriaExpression
{
    /// <summary>
    /// Criterias.
    /// </summary>
    public virtual List<Criteria> Criterias { get; } = [];

    /// <summary>
    /// Add <see cref="OperationType.Equal"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Equal<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Equal, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.NotEqual"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void NotEqual<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.NotEqual, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.StartsWith"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void StartsWith<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.StartsWith, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.EndsWith"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void EndsWith<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.EndsWith, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.GreaterThan"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void GreaterThan<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.GreaterThan, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.GreaterThanOrEqual"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void GreaterThanOrEqual<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.GreaterThanOrEqual, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.LessThan"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void LessThan<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.LessThan, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.LessThanOrEqual"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void LessThanOrEqual<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.LessThanOrEqual, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Between"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="value2">The value2.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Between<TType>(string property, TType value, TType value2, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Between, value, value2, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.IsNull"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void IsNull<TType>(string property, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By<TType>(property, OperationType.IsNull, default, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.IsNotNull"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void IsNotNull<TType>(string property, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By<TType>(property, OperationType.IsNotNull, default, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.IsEmpty"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void IsEmpty<TType>(string property, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By<TType>(property, OperationType.IsEmpty, default, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.IsNotEmpty"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void IsNotEmpty<TType>(string property, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By<TType>(property, OperationType.IsNotEmpty, default, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.IsNullOrWhiteSpace"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void IsNullOrWhiteSpace<TType>(string property, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By<TType>(property, OperationType.IsNullOrWhiteSpace, default, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.IsNotNullOrWhiteSpace"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void IsNotNullOrWhiteSpace<TType>(string property, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By<TType>(property, OperationType.IsNotNullOrWhiteSpace, default, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.In"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void In<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.In, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.NotIn"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void NotIn<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.NotIn, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Contains"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Contains<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Contains, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.NotContains"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void NotContains<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.NotContains, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Covers"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Covers<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Covers, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Crosses"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Crosses<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Crosses, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Touches"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Touches<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Touches, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Overlaps"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Overlaps<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Overlaps, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.CoveredBy"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void CoveredBy<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.CoveredBy, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Disjoint"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Disjoint<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Disjoint, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Intersects"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Intersects<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Intersects, value, default, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.Within"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void Within<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
        where TType : Geometry
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.Within, value, logicalType);
    }

    /// <summary>
    /// Add <see cref="OperationType.IsWithinDistance"/> filter.
    /// </summary>
    /// <typeparam name="TType">The type of the property.</typeparam>
    /// <param name="property">The property name.</param>
    /// <param name="value">The value.</param>
    /// <param name="distance">The distance.</param>
    /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
    public virtual void IsWithinDistance<TType>(string property, TType value, double distance, LogicalType logicalType = LogicalType.And)
        where TType : Geometry
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        this.By(property, OperationType.IsWithinDistance, value, distance, logicalType);
    }

    private void By<TType>(string property, OperationType operationType, TType value, object value2 = default, LogicalType logicalType = LogicalType.And)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        var criteria = new Criteria<TType>(property, operationType, value, value2, logicalType);

        this.Criterias
            .Add(criteria);
    }
}