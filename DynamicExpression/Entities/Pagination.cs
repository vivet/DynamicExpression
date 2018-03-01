using System.ComponentModel;
using DynamicExpression.Interfaces;

namespace DynamicExpression.Entities
{
    /// <inheritdoc />
    public class Pagination : IPagination
    {
        internal const int DEFAULT_COUNT = 25;
        internal const int DEFAULT_NUMBER = 1;

        /// <inheritdoc />
        [DefaultValue(Pagination.DEFAULT_NUMBER)]
        public virtual int? Number { get; set; } = Pagination.DEFAULT_NUMBER;

        /// <inheritdoc />
        [DefaultValue(Pagination.DEFAULT_COUNT)]
        public virtual int? Count { get; set; } = Pagination.DEFAULT_COUNT;
    }
}