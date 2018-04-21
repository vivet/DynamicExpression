using System;
using System.Collections.Generic;
using System.Linq;
using DynamicExpression.Enums;
using DynamicExpression.Interfaces;

namespace DynamicExpression.Entities
{
    /// <inheritdoc />
    public class Criteria : ICriteria
    {
        /// <inheritdoc />
        public virtual object Value { get; set; }

        /// <inheritdoc />
        public virtual object Value2 { get; set; }

        /// <inheritdoc />
        public virtual string Property { get; set; }

        /// <inheritdoc />
        public virtual LogicalType LogicalType { get; set; }

        /// <inheritdoc />
        public virtual OperationType OperationType { get; set; }
    }

    /// <inheritdoc />
    public class Criteria<TType> : Criteria
    {
        private readonly Dictionary<string, HashSet<Type>> operationTypes = new Dictionary<string, HashSet<Type>>
        {
            { "Text", new HashSet<Type> { typeof(string), typeof(char) } },
            { "Number", new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { "Boolean", new HashSet<Type> { typeof(bool) } },
            { "Date", new HashSet<Type> { typeof(DateTime), typeof(DateTimeOffset) } },
            { "Nullable", new HashSet<Type> { typeof(Nullable<>) } },
            { "Guid", new HashSet<Type> { typeof(Guid) } },
        };

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

            if (!this.GetOperationTypes(typeof(TType)).Contains(operationType))
                throw new InvalidOperationException(OperationType.ToString());

            this.Property = property;
            this.OperationType = operationType;

            if (typeof(TType).IsArray)
            {
                if (operationType != OperationType.Contains && operationType != OperationType.In)
                    throw new ArgumentException("Only 'OperationType.Contains' and 'OperationType.In' support arrays as parameters.");

                var type = typeof(List<>);
                var genericType = type.MakeGenericType(typeof(TType).GetElementType());

                this.Value = value != null ? Activator.CreateInstance(genericType, value) : null;
                this.Value2 = value2 != null ? Activator.CreateInstance(genericType, value2) : null;
            }
            else
            {
                this.Value = value;
                this.Value2 = value2;
            }

            this.LogicalType = logicalType;
        }

        /// <summary>
        /// Gets the <see cref="OperationType"/> supported by the <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/>.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        private IEnumerable<OperationType> GetOperationTypes(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var operations = new List<OperationType>();
            var typeName = type.IsArray ? type.GetElementType().Name : type.Name;
            var operationType = operationTypes.FirstOrDefault(i => i.Value.Any(v => v.Name == typeName)).Key;

            switch (operationType)
            {
                case "Text":
                    operations.AddRange(new[] { OperationType.Contains, OperationType.EndsWith, OperationType.Equal, OperationType.IsEmpty, OperationType.IsNotEmpty, OperationType.IsNotNull, OperationType.IsNotNullNorWhiteSpace, OperationType.IsNull, OperationType.IsNullOrWhiteSpace, OperationType.NotEqual, OperationType.StartsWith });
                    break;

                case "Number":
                    operations.AddRange(new[] { OperationType.Between, OperationType.Equal, OperationType.GreaterThan, OperationType.GreaterThanOrEqualTo, OperationType.LessThan, OperationType.LessThanOrEqualTo, OperationType.NotEqual });
                    break;

                case "Boolean":
                    operations.AddRange(new[] { OperationType.Equal, OperationType.NotEqual });
                    break;

                case "Date":
                    operations.AddRange(new[] { OperationType.Between, OperationType.Equal, OperationType.GreaterThan, OperationType.GreaterThanOrEqualTo, OperationType.LessThan, OperationType.LessThanOrEqualTo, OperationType.NotEqual });
                    break;

                case "Nullable":
                    operations.AddRange(new[] { OperationType.IsNull, OperationType.IsNotNull });
                    operations.AddRange(this.GetOperationTypes(Nullable.GetUnderlyingType(type)));
                    break;

                case "Guid":
                    operations.AddRange(new[] { OperationType.Equal, OperationType.NotEqual });
                    break;

                default:
                    throw new ArgumentOutOfRangeException("operationType");
            }

            if (type.IsArray)
                operations.Add(OperationType.In);

            return operations;
        }
    }
}