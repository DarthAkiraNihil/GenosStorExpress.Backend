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

    /// <summary>
    /// Метод проверки корректности фильтра ("от" меньше "до")
    /// </summary>
    /// <returns>Статус корректности фильтра</returns>
    public bool IsValid() {
        return From <= To;
    }
}