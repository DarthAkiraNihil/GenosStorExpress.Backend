namespace GenosStorExpress.Application.Wrappers.Entity.Item;

/// <summary>
/// Класс анонимной обёртки товара
/// </summary>
public class AnonymousItemWrapper: ItemWrapper {
    /// <summary>
    /// Характеристики товара
    /// </summary>
    public IDictionary<string, dynamic> Characteristics { get; set; }
    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public AnonymousItemWrapper() {
        Characteristics = new Dictionary<string, dynamic>();
    }
}