using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Entity.Users;

/// <summary>
/// Класс-обёртка данных о юридическом лице
/// </summary>
public class LegalEntityWrapper {
    /// <summary>
    /// Номер юридического лица
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// ИНН
    /// </summary>
    public long INN { get; set; }
    /// <summary>
    /// КПП
    /// </summary>
    public long KPP { get; set; }
    /// <summary>
    /// Физический адрес
    /// </summary>
    [JsonPropertyName("physical_address")]
    public string PhysicalAddress { get; set; }
    /// <summary>
    /// Юридический адрес
    /// </summary>
    [JsonPropertyName("legal_address")]
    public string LegalAddress { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public LegalEntityWrapper() {
        Id = string.Empty;
        Email = string.Empty;
        PhysicalAddress = string.Empty;
        LegalAddress = string.Empty;
    }
}