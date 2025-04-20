namespace GenosStorExpress.Application.Wrappers.Filters;

/// <summary>
/// Класс описания фильтра
/// </summary>
public class FilterDescription {
    /// <summary>
    /// Тип фильтра
    /// </summary>
    public FilterType Type { get; set; }
    /// <summary>
    /// Название ключа фильтра
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Человеческое название фильтра
    /// </summary>
    public string VerboseName { get; set; }
    /// <summary>
    /// Варианты выбора
    /// </summary>
    public IList<string>? Choices { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public FilterDescription() {
        Name = string.Empty;
        VerboseName = string.Empty;
    }
}