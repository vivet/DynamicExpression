using System.ComponentModel;
using DynamicExpression.Interfaces;

namespace DynamicExpression.Entities
{
    /// <inheritdoc />
    public class Pagination : IPagination
    {
        /// <inheritdoc />
        [DefaultValue(1)]
        public virtual int Number { get; set; } = 1;

        /// <inheritdoc />
        [DefaultValue(25)]
        public virtual int Count { get; set; } = 25;

        /// <inheritdoc />
        [DefaultValue(0)]
        public virtual int Skip => (this.Number - 1) * this.Count;
    }
}