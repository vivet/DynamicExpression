using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DynamicExpression.Enums;
using DynamicExpression.Interfaces;

namespace DynamicExpression
{
    /// <summary>
    /// CriteriaExpression Builder
    /// </summary>
    public class CriteriaBuilder
    {
        private readonly Dictionary<OperationType, Func<Expression, Expression, Expression, Expression>> expressions;
        private readonly Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>
{
{ "Trim", typeof(string).GetRuntimeMethod("Trim", new Type[0]) },
{ "ToLower", typeof(string).GetRuntimeMethod("ToLower", new Type[0]) },
{ "Contains", typeof(string).GetRuntimeMethod("Contains", new Type[0]) },
{ "EndsWith", typeof(string).GetRuntimeMethod("EndsWith", new[] { typeof(string) }) },
{ "StartsWith", typeof(string).GetRuntimeMethod("StartsWith", new[] { typeof(string) }) }
};

        /// <summary>
        /// Constructor.
        /// </summary>
        internal CriteriaBuilder()
        {
            this.expressions = new Dictionary<OperationType, Func<Expression, Expression, Expression, Expression>>
{
{ OperationType.Equal, (member, constant, constant2) => Expression.Equal(member, constant) },
{ OperationType.NotEqual, (member, constant, constant2) => Expression.NotEqual(member, constant) },
{ OperationType.GreaterThan, (member, constant, constant2) => Expression.GreaterThan(member, constant) },
{ OperationType.GreaterThanOrEqualTo, (member, constant, constant2) => Expression.GreaterThanOrEqual(member, constant) },
{ OperationType.LessThan, (member, constant, constant2) => Expression.LessThan(member, constant) },
{ OperationType.LessThanOrEqualTo, (member, constant, constant2) => Expression.LessThanOrEqual(member, constant) },
{ OperationType.Contains, (member, constant, constant2) => this.GetContainsExpression(member, constant) },
{ OperationType.StartsWith, (member, constant, constant2) => Expression.Call(member,  this.methods["StartsWith"], constant) },
{ OperationType.EndsWith, (member, constant, constant2) => Expression.Call(member,  this.methods["EndsWith"], constant) },
{ OperationType.Between, this.GetBetweenExpression },
{ OperationType.In, (member, constant, constant2) => this.GetContainsExpression(member, constant) },
{ OperationType.IsNull, (member, constant, constant2) => Expression.Equal(member, Expression.Constant(null)) },
{ OperationType.IsNotNull, (member, constant, constant2) => Expression.NotEqual(member, Expression.Constant(null)) },
{ OperationType.IsEmpty, (member, constant, constant2) => Expression.Equal(member, Expression.Constant(string.Empty)) },
{ OperationType.IsNotEmpty, (member, constant, constant2) => Expression.NotEqual(member, Expression.Constant(string.Empty)) },
{ OperationType.IsNullOrWhiteSpace, (member, constant, constant2) => this.GetIsNullOrWhiteSpaceExpression(member) },
{ OperationType.IsNotNullNorWhiteSpace, (member, constant, constant2) => this.GetIsNotNullNorWhiteSpaceExpression(member) }
};
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <typeparam name="T">Type used in the <see cref="Expression{TDelegate}"/>.</typeparam>
        /// <param name="criteriaExpression">The <see cref="CriteriaExpression"/>.</param>
        /// <returns>The <see cref="Expression{T}"/></returns>
        public Expression<Func<T, bool>> GetExpression<T>(CriteriaExpression criteriaExpression)
        where T : class
        {
            if (criteriaExpression == null)
                throw new ArgumentNullException(nameof(criteriaExpression));

            var logicalType = LogicalType.And;
            var parameter = Expression.Parameter(typeof(T), "x");

            Expression expression = null;
            foreach (var statement in criteriaExpression.Criterias)
            {
                var expr = statement.Property.Contains("[") && statement.Property.Contains("]")
                ? GetArrayExpression(parameter, statement)
                : GetExpression(parameter, statement);

                expression = expression == null ? expr : this.GetCombinedExpression(expression, expr, logicalType);
                logicalType = statement.LogicalType;
            }

            return
            Expression.Lambda<Func<T, bool>>(expression ?? Expression.Constant(true), parameter);
        }

        private Expression GetExpression(Expression expression, ICriteria criteria, string propertyName = null)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            var memberName = propertyName ?? criteria.Property;
            var member = this.GetMemberExpression(expression, memberName);
            var constant = this.GetConstantExpression(criteria.Value);
            var constant2 = this.GetConstantExpression(criteria.Value2);

            Expression expr = null;
            if (Nullable.GetUnderlyingType(member.Type) != null && criteria.Value != null)
            {
                member = Expression.Property(member, "Value");
                expr = Expression.Property(member, "HasValue");
            }

            var stringExpression = this.GetStringExpression(member, criteria.OperationType, constant, constant2);
            expr = expr != null ? Expression.AndAlso(expr, stringExpression) : stringExpression;

            if (memberName.Contains("."))
            {
                var parentName = memberName.Substring(0, memberName.IndexOf(".", StringComparison.Ordinal));
                var parentMember = this.GetMemberExpression(expression, parentName);

                expr = criteria.OperationType == OperationType.IsNull || criteria.OperationType == OperationType.IsNullOrWhiteSpace
                ? Expression.OrElse(Expression.Equal(parentMember, Expression.Constant(null)), expr)
                : Expression.AndAlso(Expression.NotEqual(parentMember, Expression.Constant(null)), expr);
            }

            return expr;
        }
        private Expression GetConstantExpression(object value = null)
        {
            if (value == null)
                return null;

            switch (value)
            {
                case string _:
                    return Expression.Call(Expression.Call(Expression.Constant(value), this.methods["Trim"]), this.methods["ToLower"]);

                default:
                    return Expression.Constant(value);
            }
        }
        private Expression GetMemberExpression(Expression expression, string propertyName)
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
        private Expression GetContainsExpression(Expression expression1, Expression expression2)
        {
            if (expression1 == null)
                throw new ArgumentNullException(nameof(expression1));

            if (expression2 == null)
                throw new ArgumentNullException(nameof(expression2));

            MethodCallExpression contains = null;

            if (expression2 is ConstantExpression constant && constant.Value is IList && constant.Value.GetType().IsGenericParameter)
            {
                var type = constant.Value.GetType();
                var method = type.GetRuntimeMethod("Contains", new[] { type.GenericTypeArguments[0] });

                contains = Expression.Call(constant, method, expression1);
            }

            return contains ?? Expression.Call(expression1, this.methods["Contains"], expression2);
        }
        private Expression GetIsNullOrWhiteSpaceExpression(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return Expression.OrElse(
            Expression.Equal(expression, Expression.Constant(null)),
            Expression.Equal(Expression.Call(expression, this.methods["Trim"]), Expression.Constant(string.Empty)));
        }
        private Expression GetIsNotNullNorWhiteSpaceExpression(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return Expression.AndAlso(
            Expression.NotEqual(expression, Expression.Constant(null)),
            Expression.NotEqual(Expression.Call(expression, this.methods["Trim"]), Expression.Constant(string.Empty)));
        }
        private Expression GetBetweenExpression(Expression expression, Expression greaterThan, Expression lessThan)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (greaterThan == null)
                throw new ArgumentNullException(nameof(greaterThan));

            if (lessThan == null)
                throw new ArgumentNullException(nameof(lessThan));

            var value = expressions[OperationType.GreaterThanOrEqualTo].Invoke(expression, greaterThan, null);
            var value2 = expressions[OperationType.LessThanOrEqualTo].Invoke(expression, lessThan, null);

            return this.GetCombinedExpression(value, value2, LogicalType.And);
        }
        private Expression GetStringExpression(Expression expression, OperationType operationType, Expression value, Expression value2)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.Type != typeof(string))
                return this.expressions[operationType].Invoke(expression, value, value2);

            switch (operationType)
            {
                case OperationType.Equal:
                case OperationType.NotEqual:
                case OperationType.Contains:
                case OperationType.StartsWith:
                case OperationType.EndsWith:
                case OperationType.GreaterThan:
                case OperationType.GreaterThanOrEqualTo:
                case OperationType.LessThan:
                case OperationType.LessThanOrEqualTo:
                case OperationType.Between:
                case OperationType.IsEmpty:
                case OperationType.IsNotNull:
                case OperationType.IsNotEmpty:
                case OperationType.In:
                    return Expression.AndAlso(
                    Expression.NotEqual(expression, Expression.Constant(null)),
                    this.expressions[operationType].Invoke(Expression.Call(Expression.Call(expression, this.methods["StartsWith"]), this.methods["ToLower"]), value, value2));

                case OperationType.IsNull:
                case OperationType.IsNullOrWhiteSpace:
                case OperationType.IsNotNullNorWhiteSpace:
                    return this.expressions[operationType].Invoke(expression, value, value2);

                default:
                    return this.expressions[operationType].Invoke(expression, value, value2);
            }
        }
        private Expression GetArrayExpression(Expression expression, ICriteria criteria)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            var baseName = criteria.Property.Substring(0, criteria.Property.IndexOf("[", StringComparison.Ordinal));
            var name = criteria.Property.Replace(baseName, "").Replace("[", "").Replace("]", "");
            var type = expression.Type.GetRuntimeProperty(baseName).PropertyType.GenericTypeArguments[0];
            var method = typeof(Enumerable).GetRuntimeMethods().First(m => m.Name == "Any" && m.GetParameters().Length == 2).MakeGenericMethod(type);
            var member = this.GetMemberExpression(expression, baseName);
            var parameter = Expression.Parameter(type, "i");
            var expr = Expression.Lambda(GetExpression(parameter, criteria, name), parameter);

            return Expression.Call(method, member, expr);
        }
        private Expression GetCombinedExpression(Expression expression1, Expression expression2, LogicalType logicalType)
        {
            if (expression1 == null)
                throw new ArgumentNullException(nameof(expression1));

            if (expression2 == null)
                throw new ArgumentNullException(nameof(expression2));

            return logicalType == LogicalType.And
            ? Expression.AndAlso(expression1, expression2)
            : Expression.OrElse(expression1, expression2);
        }
    }
}