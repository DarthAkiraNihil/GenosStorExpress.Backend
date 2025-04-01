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

    /// <summary>
    /// Метод создания замыкания фильтра
    /// </summary>
    /// <param name="filter">Первичный фильтр</param>
    /// <returns>Результат применения фильтра</returns>
    public bool CreateFilterClosure(Func<string, bool> filter) {
        return Selected.Where(filter).FirstOrDefault() != null;
    }
}