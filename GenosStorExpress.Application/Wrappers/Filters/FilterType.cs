namespace GenosStorExpress.Application.Wrappers.Filters;

/// <summary>
/// Перечисление типа фильтра
/// </summary>
public enum FilterType {
    /// <summary>
    /// Тип "Диапазон"
    /// </summary>
    Range,
    /// <summary>
    /// Фильтр "Выбор из нескольких вариантов"
    /// </summary>
    Choice,
    /// <summary>
    /// Фильтр "Наличие"
    /// </summary>
    Having
}