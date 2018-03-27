using System;
using System.Collections.Generic;
using DynamicExpression.Entities;
using DynamicExpression.Enums;
using DynamicExpression.Interfaces;

namespace DynamicExpression
{
    /// <summary>
    /// Criteria Expression.
    /// </summary>
    public class CriteriaExpression
    {
        /// <summary>
        /// Criterias
        /// </summary>
        public virtual List<ICriteria> Criterias { get; } = new List<ICriteria>();

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
        /// Add <see cref="OperationType.GreaterThanOrEqualTo"/> filter.
        /// </summary>
        /// <typeparam name="TType">The type of the property.</typeparam>
        /// <param name="property">The property name.</param>
        /// <param name="value">The value.</param>
        /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
        public virtual void GreaterThanOrEqual<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            this.By(property, OperationType.GreaterThanOrEqualTo, value, default, logicalType);
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
        /// Add <see cref="OperationType.LessThanOrEqualTo"/> filter.
        /// </summary>
        /// <typeparam name="TType">The type of the property.</typeparam>
        /// <param name="property">The property name.</param>
        /// <param name="value">The value.</param>
        /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
        public virtual void LessThanOrEqual<TType>(string property, TType value, LogicalType logicalType = LogicalType.And)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            this.By(property, OperationType.LessThanOrEqualTo, value, default, logicalType);
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
        /// Add <see cref="OperationType.IsNotNullNorWhiteSpace"/> filter.
        /// </summary>
        /// <typeparam name="TType">The type of the property.</typeparam>
        /// <param name="property">The property name.</param>
        /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
        public virtual void IsNotNullNorWhiteSpace<TType>(string property, LogicalType logicalType = LogicalType.And)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            this.By<TType>(property, OperationType.IsNotNullNorWhiteSpace, default, default, logicalType);
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
        /// Adds a new <see cref="CriteriaExpression" /> to the <see cref="CriteriaExpression" />.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="property">The property name.</param>
        /// <param name="operationType">The <see cref="OperationType"/>.</param>
        /// <param name="value">The value.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="logicalType">The <see cref="LogicalType"/>.</param>
        public virtual void By<TType>(string property, OperationType operationType, TType value, TType value2 = default, LogicalType logicalType = LogicalType.And)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            var statement = new Criteria<TType>(property, operationType, value, value2, logicalType);

            this.Criterias.Add(statement);
        }
    }
}
