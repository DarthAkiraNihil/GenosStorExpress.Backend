using System.Text.Json.Serialization;
using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item;

/// <summary>
/// Класс-обёртка основной информации о товаре
/// </summary>
public class ItemWrapper: WithModelWrapper {
    /// <summary>
    /// Номер товара
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Цена товара
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Описание товара
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Тип товара
    /// </summary>
    [JsonPropertyName("item_type")]
    public string ItemType { get; set; }
    /// <summary>
    /// Средний рейтинг товара
    /// </summary>
    [JsonPropertyName("overall_rating")]
    public double OverallRating { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public ItemWrapper() {
        Description = string.Empty;
        ItemType = string.Empty;
    }
}