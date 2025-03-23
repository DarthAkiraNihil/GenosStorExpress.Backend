namespace GenosStorExpress.API.Wrappers;

/// <summary>
/// Класс-обёртка для данных для входа
/// </summary>
public class SignInDataWrapper {
    /// <summary>
    /// Имя пользователя
    /// </summary>
    
    public string Username { get; set; }
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    
    public SignInDataWrapper() {
        Username = string.Empty;
        Password = string.Empty;
    }
}