using System.Text.Json.Serialization;
using GenosStorExpress.Application.Wrappers.Entity.Item;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка информации о товаре в заказе
/// </summary>
public class OrderItemWrapper {
    /// <summary>
    /// Товар
    /// </summary>
    public ItemWrapper Item { get; set; }
    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }
    /// <summary>
    /// Цена покупки единицы товара
    /// </summary>
    [JsonPropertyName("bought_for")]
    public double BoughtFor { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public OrderItemWrapper() {
        Item = new ItemWrapper();
    }
}