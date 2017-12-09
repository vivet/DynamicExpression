namespace DynamicExpression.Interfaces
{
    /// <summary>
    /// Pagination.
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// Number.
        /// </summary>
        int Number { get; set; }

        /// <summary>
        /// Count (Take).
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// Skip (Skip).
        /// </summary>
        int Skip { get; }
    }
}