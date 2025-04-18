using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка краткой информации о заказа
/// </summary>
public class ShortOrderWrapper {
    /// <summary>
    /// Номер заказа
    /// </summary>
    [JsonPropertyName("order_id")]
    public long OrderId { get; set; }
    /// <summary>
    /// Статус заказа
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// Дата создания
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Количество товаров в заказе
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public ShortOrderWrapper() {
        Status = string.Empty;
    }
}