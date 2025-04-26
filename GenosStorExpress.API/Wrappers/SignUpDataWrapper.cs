using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenosStorExpress.API.Wrappers;

/// <summary>
/// Класс-обёртка для данных регистрации
/// </summary>
public class SignUpDataWrapper {
    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Тип регистрируемой роли. Может принимать значения:
    /// <list type="bullet">
    ///     <item>
    ///         <term>
    ///             individual_entity
    ///         </term>
    ///         <description>
    ///             Физическое лицо
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>
    ///            legal_entity
    ///         </term>
    ///         <description>
    ///             Юридическое лицо
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    [JsonPropertyName("user_type")]
    public string UserType { get; set; }
    /// <summary>
    /// Дополнительные регистрационные данные. Для разных типов регистрируемой роли, данные разные
    /// Для физического лица:
    ///     <code>
    ///     {
    ///         "name": "string", // Имя
    ///         "surname": "string", // Фамилия
    ///         "phone_number": "string" // Номер телефона
    ///     }
    ///     </code>
    /// Для юридического лица лица:
    ///     <code>
    ///     {
    ///         "inn": 0, // ИНН
    ///         "kpp": 0, // КПП
    ///         "physical_address": "string" // Физический адрес
    ///         "legal_address": "string" // Юридический адрес
    ///     }
    ///     </code>
    /// </summary>
    [JsonPropertyName("additional_data")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    public JsonElement AdditionalData { get; set; }
    //public IDictionary<string, string> AdditionalData { get; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public SignUpDataWrapper() {
        Email = string.Empty;
        Password = string.Empty;
        UserType = string.Empty;
        AdditionalData = new JsonElement();
        //AdditionalData = new Dictionary<string, string>();
    }
}