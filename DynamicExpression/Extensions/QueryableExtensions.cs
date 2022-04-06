using System;
using System.Linq;
using System.Linq.Expressions;
using DynamicExpression.Enums;
using DynamicExpression.Interfaces;

namespace DynamicExpression.Extensions
{
    /// <summary>
    /// Queryable Extensions.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Adds order and limit clauses to the <see cref="IQueryable{T}"/> based on the passed <paramref name="query"/>.
        /// </summary>
        /// <typeparam name="T">The type used in the <see cref="IQueryable{T}"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/>.</param>
        /// <param name="query">The <see cref="IQuery"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, IQuery query)
            where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (query == null)
                throw new ArgumentNullException(nameof(query));

            return source
                .Order(query.Order)
                .Limit(query.Paging);
        }

        /// <summary>
        /// Adds where clause to the <see cref="IQueryable{T}"/> based on the passed <paramref name="queryCriteria"/>.
        /// </summary>
        /// <typeparam name="T">The type used in the <see cref="IQueryable{T}"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/>.</param>
        /// <param name="queryCriteria">The <see cref="IQueryCriteria"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, IQueryCriteria queryCriteria)
            where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (queryCriteria == null)
                throw new ArgumentNullException(nameof(queryCriteria));

            var criteria = queryCriteria.GetExpressions();
            var expression = new CriteriaBuilder().Build<T>(criteria);

            return source
                .Where(expression);
        }

        /// <summary>
        /// Adds where, order and limit clauses to the <see cref="IQueryable{T}"/> based on the passed <paramref name="queryCriteria"/>.
        /// </summary>
        /// <typeparam name="T">The type used in the <see cref="IQueryable{T}"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/>.</param>
        /// <param name="queryCriteria">The <see cref="IQuery{TCriteria}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, IQuery<IQueryCriteria> queryCriteria)
            where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (queryCriteria == null)
                throw new ArgumentNullException(nameof(queryCriteria));

            var criteria = queryCriteria.Criteria.GetExpressions();
            var expression = new CriteriaBuilder().Build<T>(criteria);

            return source
                .Where(expression)
                .Order(queryCriteria.Order)
                .Limit(queryCriteria.Paging);
        }

        /// <summary>
        /// Adds order by clause to the <see cref="IQueryable{T}"/> based on the passed <paramref name="ordering"/>
        /// </summary>
        /// <typeparam name="T">The type used in the <see cref="IQueryable{T}"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/>.</param>
        /// <param name="ordering">The <see cref="IOrdering"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> Order<T>(this IQueryable<T> source, IOrdering ordering)
            where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (ordering == null)
                throw new ArgumentNullException(nameof(ordering));

            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, ordering.By);
            var expression = Expression.Lambda<Func<T, dynamic>>(Expression.Convert(property, typeof(object)), parameter);
            return ordering.Direction == OrderingDirection.Asc ? source.OrderBy(expression) : source.OrderByDescending(expression);
        }

        /// <summary>
        /// Adds skip and take clauses to the <see cref="IQueryable{T}"/> based on the passed <paramref name="pagination"/>.
        /// </summary>
        /// <typeparam name="T">The type used in the <see cref="IQueryable{T}"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/>.</param>
        /// <param name="pagination">The <see cref="IPagination"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> Limit<T>(this IQueryable<T> source, IPagination pagination)
            where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (pagination == null)
                throw new ArgumentNullException(nameof(pagination));

            var count = pagination.Count;
            var number = pagination.Number;

            return source
                .Skip((number - 1) * count)
                .Take(count);
        }
    }
}