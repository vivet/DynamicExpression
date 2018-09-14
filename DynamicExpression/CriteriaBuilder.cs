using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DynamicExpression.Enums;
using DynamicExpression.Interfaces;

namespace DynamicExpression
{
    /// <summary>
    /// Criteria Builder
    /// </summary>
    public class CriteriaBuilder
    {
        /// <summary>
        /// Builds the <see cref="CriteriaExpression"/>'s, and returns an <see cref="Expression"/>.
        /// </summary>
        /// <typeparam name="T">Type used in the <see cref="Expression{TDelegate}"/>.</typeparam>
        /// <param name="criteriaExpression">The <see cref="CriteriaExpression"/>.</param>
        /// <returns>The <see cref="Expression{T}"/></returns>
        public virtual Expression<Func<T, bool>> Build<T>(CriteriaExpression criteriaExpression)
            where T : class
        {
            if (criteriaExpression == null)
                throw new ArgumentNullException(nameof(criteriaExpression));

            var logicalType = LogicalType.And;
            var parameter = Expression.Parameter(typeof(T), "x");

            Expression expression = null;
            foreach (var statement in criteriaExpression.Criterias)
            {
                Expression innerExpression;
                if (statement.Property.Contains("[") && statement.Property.Contains("]"))
                {
                    var baseName = statement.Property.Substring(0, statement.Property.IndexOf("[", StringComparison.Ordinal));
                    var name = statement.Property.Replace(baseName, "").Replace("[", "").Replace("]", "");
                    var type = parameter.Type.GetRuntimeProperty(baseName).PropertyType.GenericTypeArguments[0];
                    var method = typeof(Enumerable).GetRuntimeMethods().First(m => m.Name == "Any" && m.GetParameters().Length == 2).MakeGenericMethod(type);
                    var member = this.GetMember(parameter, baseName);
                    var parameter2 = Expression.Parameter(type, "i");
                    var expr2 = Expression.Lambda(GetExpression(parameter2, statement, name), parameter2);

                    innerExpression = Expression.Call(method, member, expr2);
                }
                else
                {
                    innerExpression = this.GetExpression(parameter, statement);
                }

                expression = expression == null 
                    ? innerExpression 
                    : logicalType == LogicalType.And
                        ? Expression.AndAlso(expression, innerExpression)
                        : Expression.OrElse(expression, innerExpression);

                logicalType = statement.LogicalType;
            }

            return Expression.Lambda<Func<T, bool>>(expression ?? Expression.Constant(true), parameter);
        }

        private Expression GetMember(Expression expression, string propertyName)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            while (true)
            {
                if (propertyName.Contains("."))
                {
                    var index = propertyName.IndexOf(".", StringComparison.Ordinal);
                    var param = Expression.Property(expression, propertyName.Substring(0, index));

                    expression = param;
                    propertyName = propertyName.Substring(index + 1);

                    continue;
                }

                return Expression.Property(expression, propertyName);
            }
        }
        private Expression GetExpression(Expression parameter, ICriteria criteria, string propertyName = null)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            var memberName = propertyName ?? criteria.Property;
            var member = this.GetMember(parameter, memberName);
            var value = Expression.Constant(criteria.Value) as Expression;
            var value2 = Expression.Constant(criteria.Value2);
            var operationType = criteria.OperationType;

            if (Nullable.GetUnderlyingType(member.Type) != null)
            {
                if (Nullable.GetUnderlyingType(value.Type) == null)
                {
                    value = Expression.Constant(criteria.Value, typeof(Guid?));
                }
            }
            else if (Nullable.GetUnderlyingType(value.Type) != null)
            {
                if (Nullable.GetUnderlyingType(member.Type) == null)
                {
                    value = Expression.Constant(value, typeof(Guid));
                }
            }

            if (member.Type.IsEnum)
            {
                var expression = Expression.Convert(member, Enum.GetUnderlyingType(member.Type));
                value = Expression.Convert(value, Enum.GetUnderlyingType(value.Type));

                switch (operationType)
                {
                    case OperationType.Contains:
                        return Expression.Equal(Expression.Or(expression, value), value);

                    case OperationType.NotContains:
                        return Expression.Not(Expression.Equal(Expression.Or(expression, value), value));

                    case OperationType.Equal:
                        return Expression.Equal(Expression.And(expression, value), value);

                    case OperationType.NotEqual:
                        return Expression.NotEqual(Expression.And(expression, value), value);
                }
            }
            else
            {
                switch (operationType)
                {
                    case OperationType.Equal:
                        if (Nullable.GetUnderlyingType(member.Type) == null && member.Type != typeof(string))
                            return Expression.Equal(member, value);

                        return Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.Equal(member, value));

                    case OperationType.NotEqual:
                        if (Nullable.GetUnderlyingType(member.Type) == null && member.Type != typeof(string))
                            return Expression.NotEqual(member, value);

                        return Expression.OrElse(Expression.Equal(member, Expression.Constant(null)), Expression.NotEqual(member, value));

                    case OperationType.StartsWith:
                        return Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.Call(member, typeof(string).GetRuntimeMethod("StartsWith", new[] { typeof(string) }), value));

                    case OperationType.EndsWith:
                        return Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.Call(member, typeof(string).GetRuntimeMethod("EndsWith", new[] { typeof(string) }), value));

                    case OperationType.GreaterThan:
                        return Expression.GreaterThan(member, value);

                    case OperationType.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, value);

                    case OperationType.LessThan:
                        return Expression.LessThan(member, value);

                    case OperationType.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, value);

                    case OperationType.Between:
                        return Expression.AndAlso(Expression.GreaterThanOrEqual(member, value), Expression.LessThanOrEqual(member, value2));

                    case OperationType.IsNull:
                        return Expression.Equal(member, Expression.Constant(null));

                    case OperationType.IsEmpty:
                        return Expression.Equal(member, Expression.Constant(string.Empty));

                    case OperationType.IsNotNull:
                        return Expression.NotEqual(member, Expression.Constant(null));

                    case OperationType.IsNotEmpty:
                        return Expression.NotEqual(member, Expression.Constant(string.Empty));

                    case OperationType.Contains:
                        return Expression.Call(member, typeof(string).GetRuntimeMethod("Contains", new[] { value.Type }), value);

                    case OperationType.NotContains:
                        return Expression.Not(Expression.Call(member, typeof(string).GetRuntimeMethod("Contains", new[] { value.Type }), value));

                    case OperationType.IsNullOrWhiteSpace:
                        return Expression.OrElse(
                            Expression.Equal(member, Expression.Constant(null)), 
                            Expression.Equal(Expression.Call(member, typeof(string).GetRuntimeMethod("Trim", new Type[0])), Expression.Constant(string.Empty)));

                    case OperationType.IsNotNullOrWhiteSpace:
                        return Expression.AndAlso(
                            Expression.NotEqual(member, Expression.Constant(null)),
                            Expression.NotEqual(Expression.Call(member, typeof(string).GetRuntimeMethod("Trim", new Type[0])), Expression.Constant(string.Empty)));
                }
            }

            throw new NotSupportedException($"'{operationType}' is not supported by '{value.Type}' ");
        }
    }
}