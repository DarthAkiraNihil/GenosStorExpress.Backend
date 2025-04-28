using GenosStorExpress.Domain.Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GenosStorExpress.API.Controllers;

/// <summary>
/// Базовый контроллер для всех остальных контроллеров
/// </summary>
public abstract class AbstractController: ControllerBase {
    /// <summary>
    /// Менеджер пользователей
    /// </summary>
    protected readonly UserManager<User> _userManager;
    protected readonly ILogger _logger;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="logger">Логгер</param>
    protected AbstractController(UserManager<User> userManager, ILogger<AbstractController> logger) {
        _userManager = userManager;
        _logger = logger;
    }
    
    /// <summary>
    /// Вспомогательный метод для получения текущего пользователя
    /// </summary>
    /// <returns>Текущий пользователь или null</returns>
    protected User? _getCurrentUser() {
        var task = _userManager.GetUserAsync(HttpContext.User);
        task.Wait();
        if (task.IsCompletedSuccessfully) {
            return task.Result;
        }
        return null;
    }

    /// <summary>
    /// Метод логирования сообщения
    /// </summary>
    /// <param name="methodName">Метод контроллера, откуда было вызвано логирование</param>
    /// <param name="parameters">Список параметров метода со значениями</param>
    /// <param name="detail">Сообщение</param>
    /// <param name="user">Текущий пользователь</param>
    /// <param name="isError">Флаг ошибки</param>
    protected void _log(string methodName, IList<string> parameters, string detail, User? user = null, bool isError = false) {
        string formattedParameters = string.Join(", ", parameters);
        if (isError) {
            _logger.LogError($"Executed {methodName} ({Request.Method} {Request.Path}). User = {user?.Email}, Status = Error: {detail}. Params: {formattedParameters}");
        } else {
            if (detail.Length == 0) {
                _logger.LogInformation($"Executed {methodName} ({Request.Method} {Request.Path}). User = {user?.Email}, Status = Success. Params: {formattedParameters}");
            } else {
                _logger.LogInformation($"Executed {methodName} ({Request.Method} {Request.Path}). User = {user?.Email}, Status = Success. Detail = {detail}, Params: {formattedParameters}");
            }
        }
    }
}