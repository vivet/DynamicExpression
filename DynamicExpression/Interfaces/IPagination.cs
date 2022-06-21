namespace DynamicExpression.Interfaces;

/// <summary>
/// Pagination interface.
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
}