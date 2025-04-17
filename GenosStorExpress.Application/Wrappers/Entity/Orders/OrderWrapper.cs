using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка информации о заказе
/// </summary>
public class OrderWrapper {
    /// <summary>
    /// Номер заказа
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Список товаров в заказе
    /// </summary>
    public IList<OrderItemWrapper> Items { get; set; }
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
    /// Стандартный конструктор
    /// </summary>
    public OrderWrapper() {
        Items = new List<OrderItemWrapper>();
        Status = string.Empty;
        CreatedAt = DateTime.Now;
    }
}