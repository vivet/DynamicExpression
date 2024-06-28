using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DynamicExpression.Entities;
using DynamicExpression.Enums;
using DynamicExpression.Extensions;

namespace DynamicExpression;

/// <summary>
/// Criteria Builder
/// </summary>
public class CriteriaBuilder
{
    /// <summary>
    /// Builds the <see cref="CriteriaExpression"/>, and returns an <see cref="Expression"/>.
    /// </summary>
    /// <typeparam name="T">Type used in the <see cref="Expression{TDelegate}"/>.</typeparam>
    /// <param name="criteriaExpression">The <see cref="CriteriaExpression"/>.</param>
    /// <returns>The <see cref="Expression{T}"/></returns>
    public virtual Expression<Func<T, bool>> Build<T>(CriteriaExpression criteriaExpression)
        where T : class
    {
        if (criteriaExpression == null)
            throw new ArgumentNullException(nameof(criteriaExpression));

        var parameter = Expression.Parameter(typeof(T), "x");
        var expression = this.BuildExpression(criteriaExpression, parameter);

        return Expression.Lambda<Func<T, bool>>(expression ?? Expression.Constant(true), parameter);
    }

    /// <summary>
    /// Builds the <see cref="CriteriaExpression"/>'s, and returns an <see cref="Expression"/>.
    /// </summary>
    /// <typeparam name="T">Type used in the <see cref="Expression{TDelegate}"/>.</typeparam>
    /// <param name="criteriaExpressions">The <see cref="CriteriaExpression"/>'s.</param>
    /// <returns>The <see cref="Expression{T}"/></returns>
    public virtual Expression<Func<T, bool>> Build<T>(IEnumerable<CriteriaExpression> criteriaExpressions)
        where T : class
    {
        if (criteriaExpressions == null)
            throw new ArgumentNullException(nameof(criteriaExpressions));

        var parameter = Expression.Parameter(typeof(T), "x");
        var expressionCombined = criteriaExpressions
            .Select(x => this.BuildExpression(x, parameter))
            .Aggregate<Expression, Expression>(null, (current, expression) => expression == null
                ? current
                : current == null
                    ? expression
                    : Expression.AndAlso(current, expression));

        return Expression.Lambda<Func<T, bool>>(expressionCombined ?? Expression.Constant(true), parameter);
    }

    private Expression GetMember(Expression parameter, string propertyName)
    {
        if (parameter == null)
            throw new ArgumentNullException(nameof(parameter));

        if (propertyName == null)
            throw new ArgumentNullException(nameof(propertyName));

        while (true)
        {
            if (propertyName.Contains("."))
            {
                var index = propertyName.IndexOf(".", StringComparison.Ordinal);
                var param = Expression.Property(parameter, propertyName[..index]);

                parameter = param;
                propertyName = propertyName[(index + 1)..];

                continue;
            }

            return Expression.Property(parameter, propertyName);
        }
    }
    private Expression GetExpression(Expression parameter, Criteria criteria, string propertyName = null)
    {
        if (parameter == null)
            throw new ArgumentNullException(nameof(parameter));

        if (criteria == null)
            throw new ArgumentNullException(nameof(criteria));

        var name = propertyName ?? criteria.Property;
        var member = this.GetMember(parameter, name);
        var value = Expression.Constant(criteria.Value) as Expression;
        var value2 = Expression.Constant(criteria.Value2);
        var operationType = criteria.OperationType;

        if (Nullable.GetUnderlyingType(member.Type) != null)
        {
            if (Nullable.GetUnderlyingType(value.Type) == null)
            {
                value = Expression.Constant(criteria.Value, member.Type);
                value2 = Expression.Constant(criteria.Value2, member.Type);
            }
        }
        else if (Nullable.GetUnderlyingType(value.Type) != null)
        {
            if (Nullable.GetUnderlyingType(member.Type) == null)
            {
                value = Expression.Constant(criteria.Value, value.Type);
                value2 = Expression.Constant(criteria.Value2, value.Type);
            }
        }

        if (member.Type.IsEnum)
        {
            var expression = Expression.Convert(member, Enum.GetUnderlyingType(member.Type));
            value = Expression.Convert(value, Enum.GetUnderlyingType(value.Type));

            switch (operationType)
            {
                case OperationType.In:
                case OperationType.Contains:
                    return Expression.Equal(Expression.Or(expression, value), value);

                case OperationType.NotIn:
                case OperationType.NotContains:
                    return Expression.Not(Expression.Equal(Expression.Or(expression, value), value));

                case OperationType.Equal:
                    return Expression.Equal(expression, value);

                case OperationType.NotEqual:
                    return Expression.NotEqual(expression, value);
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
                    var methodStartsWith = typeof(string).GetRuntimeMethod("StartsWith", new[] { typeof(string) });
                    
                    if (methodStartsWith == null)
                    {
                        throw new NullReferenceException(nameof(methodStartsWith));
                    }

                    return Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.Call(member, methodStartsWith, value));

                case OperationType.EndsWith:
                    var methodEndsWith = typeof(string).GetRuntimeMethod("EndsWith", new[] { typeof(string) });

                    if (methodEndsWith == null)
                    {
                        throw new NullReferenceException(nameof(methodEndsWith));
                    }
                    
                    return Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.Call(member, methodEndsWith, value));

                case OperationType.GreaterThan:
                    return Nullable.GetUnderlyingType(member.Type) == null
                        ? Expression.GreaterThan(member, value)
                        : Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.GreaterThan(member, value));

                case OperationType.GreaterThanOrEqual:
                    return Nullable.GetUnderlyingType(member.Type) == null
                        ? Expression.GreaterThanOrEqual(member, value)
                        : Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.GreaterThanOrEqual(member, value));

                case OperationType.LessThan:
                    return Nullable.GetUnderlyingType(member.Type) == null
                        ? Expression.LessThan(member, value)
                        : Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.LessThan(member, value));

                case OperationType.LessThanOrEqual:
                    return Nullable.GetUnderlyingType(member.Type) == null
                        ? Expression.LessThanOrEqual(member, value)
                        : Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.LessThanOrEqual(member, value));

                case OperationType.Between:
                    return Nullable.GetUnderlyingType(member.Type) == null
                        ? Expression.AndAlso(Expression.GreaterThanOrEqual(member, value), Expression.LessThanOrEqual(member, value2))
                        : Expression.AndAlso(Expression.NotEqual(member, Expression.Constant(null)), Expression.AndAlso(Expression.GreaterThanOrEqual(member, value), Expression.LessThanOrEqual(member, value2)));

                case OperationType.IsNull:
                    return Expression.Equal(member, Expression.Constant(null));

                case OperationType.IsEmpty:
                    return Expression.Equal(member, Expression.Constant(string.Empty));

                case OperationType.IsNotNull:
                    return Expression.NotEqual(member, Expression.Constant(null));

                case OperationType.IsNotEmpty:
                    return Expression.NotEqual(member, Expression.Constant(string.Empty));

                case OperationType.In:
                case OperationType.Contains:
                {
                    MethodInfo methodContains;
                    if (value.Type.IsArrayOrEnumerable()) 
                    {
                        var constant = (ConstantExpression)value;

                        if (constant.Value == null)
                        {
                            throw new NullReferenceException(nameof(constant.Value));
                        }

                        var elementType = value.Type.GetGenericArguments().FirstOrDefault() ?? constant.Type.GetElementType();

                        if (elementType == null)
                        {
                            throw new NullReferenceException(nameof(elementType));
                        }

                        methodContains = typeof(IList).GetRuntimeMethod("Contains", new[] { elementType });

                        if (methodContains == null)
                        {
                            throw new NullReferenceException(nameof(methodContains));
                        }
                        
                        return Expression.Call(constant, methodContains, member);
                    }

                    methodContains = typeof(string).GetRuntimeMethod("Contains", new[] { value.Type });

                    if (methodContains == null)
                    {
                        throw new NullReferenceException(nameof(methodContains));
                    }
                    
                    return Expression.Call(member, methodContains, value);
                }

                case OperationType.NotIn:
                case OperationType.NotContains:
                {
                    MethodInfo methodNotContains;
                    if (value.Type.IsArrayOrEnumerable())
                    {
                        var constant = (ConstantExpression)value;

                        if (constant.Value == null)
                        {
                            throw new NullReferenceException(nameof(constant.Value));
                        }

                        var elementType = constant.Type.GetGenericArguments().FirstOrDefault() ?? constant.Type.GetElementType();

                        if (elementType == null)
                        {
                            throw new NullReferenceException(nameof(elementType));
                        }

                        methodNotContains = typeof(IList).GetRuntimeMethod("Contains", new[] { elementType });

                        if (methodNotContains == null)
                        {
                            throw new NullReferenceException(nameof(methodNotContains));
                        }

                        return Expression.Not(Expression.Call(constant, methodNotContains, member));
                    }

                    methodNotContains = typeof(string).GetRuntimeMethod("Contains", new[] { value.Type });

                    if (methodNotContains == null)
                    {
                        throw new NullReferenceException(nameof(methodNotContains));
                    }
                    
                    return Expression.Not(Expression.Call(member, methodNotContains, value));
                }

                case OperationType.IsNullOrWhiteSpace:
                    var methodTrim = typeof(string).GetRuntimeMethod("Trim", Type.EmptyTypes);

                    if (methodTrim == null)
                    {
                        throw new NullReferenceException(nameof(methodTrim));
                    }

                    return Expression.OrElse(
                        Expression.Equal(member, Expression.Constant(null)),
                        Expression.Equal(Expression.Call(member, methodTrim), Expression.Constant(string.Empty)));

                case OperationType.IsNotNullOrWhiteSpace:
                    var methodTrim2 = typeof(string).GetRuntimeMethod("Trim", Type.EmptyTypes);

                    if (methodTrim2 == null)
                    {
                        throw new NullReferenceException(nameof(methodTrim2));
                    }

                    return Expression.AndAlso(
                        Expression.NotEqual(member, Expression.Constant(null)),
                        Expression.NotEqual(Expression.Call(member, methodTrim2), Expression.Constant(string.Empty)));

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        throw new NotSupportedException($"'{operationType}' is not supported by '{value.Type}' ");
    }
    private Expression BuildExpression(CriteriaExpression criteriaExpression, Expression parameter)
    {
        var prevLogicalType = LogicalType.And;

        Expression expression = null;
        foreach (var criteria in criteriaExpression.Criterias)
        {
            Expression innerExpression;
            if (criteria.Property.Contains("[") && criteria.Property.Contains("]"))
            {
                var startArray = criteria.Property.IndexOf("[", StringComparison.Ordinal);
                var finishArray = criteria.Property.IndexOf("]", StringComparison.Ordinal);
                var baseName = criteria.Property[..startArray];
                var lastIndexOfDot = baseName.LastIndexOf(".", StringComparison.Ordinal);

                Expression paramNested;
                if (lastIndexOfDot > 0)
                {
                    paramNested = this.GetMember(parameter, baseName[..lastIndexOfDot]);
                    baseName = baseName[(lastIndexOfDot + 1)..];
                }
                else
                {
                    paramNested = parameter;
                }

                var name = criteria.Property.Substring(startArray + 1, finishArray - startArray - 1);
                var property = paramNested.Type.GetRuntimeProperty(baseName);

                if (property == null)
                {
                    throw new NullReferenceException(nameof(property));
                }
                
                var type = property.PropertyType.GenericTypeArguments[0];
                var methodAny = typeof(Enumerable).GetRuntimeMethods().First(x => x.Name == "Any" && x.GetParameters().Length == 2).MakeGenericMethod(type);
                var memberAny = this.GetMember(paramNested, baseName);
                var parameterAny = Expression.Parameter(type, "i");
                var expressionAny = this.GetExpression(parameterAny, criteria, name);
                var expr2 = Expression.Lambda(expressionAny, parameterAny);

                innerExpression = Expression.Call(methodAny, memberAny, expr2);
            }
            else
            {
                innerExpression = this.GetExpression(parameter, criteria);
            }

            expression = expression == null
                ? innerExpression
                : prevLogicalType == LogicalType.And
                    ? Expression.AndAlso(expression, innerExpression)
                    : Expression.OrElse(expression, innerExpression);

            prevLogicalType = criteria.LogicalType;
        }

        return expression;
    }
}