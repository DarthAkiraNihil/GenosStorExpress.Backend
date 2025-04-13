using System.Text.Json.Serialization;
using GenosStorExpress.Application.Wrappers.Entity.Item;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Обёртка товара в корзмне
/// </summary>
public class CartItemWrapper {
    /// <summary>
    /// Товар в корзине
    /// </summary>
    [JsonPropertyName("item")]
    public ItemWrapper Item { get; set; }
    /// <summary>
    /// Количество товара в корзине
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public CartItemWrapper() {
        Item = new ItemWrapper();
    }
}