using DynamicExpression.Interfaces;

namespace DynamicExpression
{
    /// <inheritdoc />
    public class Pagination : IPagination
    {
        /// <inheritdoc />
        public virtual int Number { get; set; } = 1;

        /// <inheritdoc />
        public virtual int Count { get; set; } = 25;

        /// <inheritdoc />
        public virtual int Skip => (this.Number - 1) * this.Count;
    }
}