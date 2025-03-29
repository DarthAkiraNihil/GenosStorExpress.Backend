namespace GenosStorExpress.Application.Wrappers.Filters;


/// <summary>
/// Класс-обёртка для фильтра-выборки из списка вариантов
/// </summary>
public class ChoiceFilterWrapper {
    
    /// <summary>
    /// Список выбранный вариантов
    /// </summary>
    public IList<string> Selected { get; set; }
    
    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public ChoiceFilterWrapper() {
        Selected = new List<string>();
    }
}