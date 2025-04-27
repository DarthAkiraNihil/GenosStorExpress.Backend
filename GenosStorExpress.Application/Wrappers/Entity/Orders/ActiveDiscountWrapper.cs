using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Обёртка скидки на товар
/// </summary>
public class ActiveDiscountWrapper {
    /// <summary>
    /// Номер скидки
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Размер скидки
    /// </summary>
    public double Value { get; set; }
    /// <summary>
    /// Дата окончания скидки
    /// </summary>
    [JsonPropertyName("ends_at")]
    public DateTime EndsAt { get; set; }
}