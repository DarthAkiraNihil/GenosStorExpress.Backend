namespace GenosStorExpress.Application.Wrappers.Entity.Item;

/// <summary>
/// Класс-обёртка для отзыва на товар
/// </summary>
public class ReviewWrapper {
    /// <summary>
    /// Оценка товара
    /// </summary>
    public byte Rating { get; set; }
    /// <summary>
    /// Комментарий к товару
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public ReviewWrapper() {
        Comment = string.Empty;
    }
}