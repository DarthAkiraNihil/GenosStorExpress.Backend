namespace GenosStorExpress.Application.Wrappers.Filters;

/// <summary>
/// Класс-обёртка для фильтра-диапазона
/// </summary>
public class RangeFilterWrapper {
    /// <summary>
    /// "От"
    /// </summary>
    public int From { get; set; }
    /// <summary>
    /// "До"
    /// </summary>
    public int To { get; set; }
}