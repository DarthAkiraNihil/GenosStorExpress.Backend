﻿using System.Text.Json.Serialization;
using GenosStorExpress.Application.Wrappers.Entity.Base;
using GenosStorExpress.Application.Wrappers.Entity.Orders;

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
    /// Информация о скидке на товар
    /// </summary>
    [JsonPropertyName("active_discount")]
    public ActiveDiscountWrapper? ActiveDiscount { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public ItemWrapper() {
        Description = string.Empty;
        ItemType = string.Empty;
    }
}