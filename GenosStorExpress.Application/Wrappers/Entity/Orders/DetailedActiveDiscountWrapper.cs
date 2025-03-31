using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка данных для создания скидки
/// </summary>
public class DetailedActiveDiscountWrapper {
    /// <summary>
    /// Номер скидки
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Величина скидки
    /// </summary>
    public double Value { get; set; }
    
    /// <summary>
    /// Дата окончания скидки
    /// </summary>
    [JsonPropertyName("ends_at")]
    public DateTime EndsAt { get; set; }
    
    /// <summary>
    /// Список предметов, для которых применяется скидка
    /// </summary>
    [JsonPropertyName("for_items")]
    public IList<int> ForItems { get; set; }
    
    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public DetailedActiveDiscountWrapper() {
        EndsAt = DateTime.MinValue;
        ForItems = new List<int>();
    }
}