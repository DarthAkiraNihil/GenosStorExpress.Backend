namespace GenosStorExpress.Application.Wrappers.Filters;

/// <summary>
/// Класс-обёртка для структурированного контейнера всех фильтрой
/// </summary>
public class FilterContainerWrapper {
    
    /// <summary>
    /// Фильтр по имени
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Фильтры-выборки
    /// </summary>
    public IDictionary<string, ChoiceFilterWrapper> Choices { get; set; }
    /// <summary>
    /// Фильтры-диапазоны
    /// </summary>
    public IDictionary<string, RangeFilterWrapper> Ranges { get; set; }
    /// <summary>
    /// Фильтры наличия признака
    /// </summary>
    public IDictionary<string, bool> Havings { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public FilterContainerWrapper() {
        Name = String.Empty;
        Choices = new Dictionary<string, ChoiceFilterWrapper>();
        Ranges = new Dictionary<string, RangeFilterWrapper>();
        Havings = new Dictionary<string, bool>();
    }
}