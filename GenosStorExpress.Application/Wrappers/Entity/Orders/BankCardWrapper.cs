using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка банковской карты
/// </summary>
public class BankCardWrapper {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Номер карты
    /// </summary>
    public long Number { get; set; }
    /// <summary>
    /// Месяц срока действия
    /// </summary>
    [JsonPropertyName("valid_thru_month")]
    public byte ValidThruMonth { get; set; }
    /// <summary>
    /// Год срока действия
    /// </summary>
    [JsonPropertyName("valid_thru_year")]
    public byte ValidThruYear { get; set; }
    /// <summary>
    /// CVC
    /// </summary>
    public byte CVC { get; set; }
    /// <summary>
    /// Владелец карты
    /// </summary>
    public string Owner { get; set; }
    /// <summary>
    /// Банковская система
    /// </summary>
    [JsonPropertyName("bank_system")]
    public string BankSystem { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public BankCardWrapper() {
        Owner = string.Empty;
        BankSystem = string.Empty;
    }
}