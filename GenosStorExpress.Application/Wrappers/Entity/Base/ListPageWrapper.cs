namespace GenosStorExpress.Application.Wrappers.Entity.Base;

/// <summary>
/// Класс-обёртка для пагинированного списка сущностей
/// </summary>
/// <typeparam name="T">Тип пагинируемой сущности</typeparam>
public class ListPageWrapper<T> where T: class {
    /// <summary>
    /// Общее количество сущностей
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Ссылка на предыдущую страницу
    /// </summary>
    public string? Previous { get; set; }
    /// <summary>
    /// Ссылка на следующую страницу
    /// </summary>
    public string? Next { get; set; }
    /// <summary>
    /// Страница списка
    /// </summary>
    public IList<T> Items { get; set; } = new List<T>();
}