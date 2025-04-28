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
}