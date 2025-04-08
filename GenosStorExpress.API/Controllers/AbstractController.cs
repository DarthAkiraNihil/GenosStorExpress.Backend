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

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    protected AbstractController(UserManager<User> userManager) {
        _userManager = userManager;
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