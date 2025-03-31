
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GenosStorExpress.API.Controllers;

/// <summary>
/// Контроллер <c>DiscountController</c> предоставляет методы для управления скидами на товары
/// </summary>
[Route("api/discount")]
[ApiController]
public class DiscountController : AbstractController {
    
    private readonly IActiveDiscountService _activeDiscountService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="activeDiscountService">Сервис скидок</param>
    public DiscountController(UserManager<User> userManager, IActiveDiscountService activeDiscountService) : base(userManager) {
        _activeDiscountService = activeDiscountService;
    }

    /// <summary>
    /// Создание скидки. Только под администратором
    /// </summary>
    /// <param name="data">Данные создаваемой скидки</param>
    /// <returns>Созданную скидку</returns>
    [Authorize(Roles = "administrator")]
    [HttpPost]
    public ActionResult<DetailedActiveDiscountWrapper> Create([FromBody] DetailedActiveDiscountWrapper data) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            _activeDiscountService.Create(data);
            return Ok(data);
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Получение списка скидок. Только под администратором
    /// </summary>
    /// <returns>Список активных скидок</returns>
    [Authorize(Roles = "administrator")]
    [HttpGet]
    public ActionResult<IEnumerable<ActiveDiscountWrapper>> List() {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            return Ok(_activeDiscountService.List());
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Получение информации о конкретной скидке. Только под администратором
    /// </summary>
    /// <param name="id">Номер скидки</param>
    /// <returns>Скидку</returns>
    [Authorize(Roles = "administrator")]
    [HttpGet("{id:int}")]
    public ActionResult<DetailedActiveDiscountWrapper> Get(int id) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            return Ok(_activeDiscountService.Get(id));
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Редактирование скидки. Только под администратором
    /// </summary>
    /// <param name="id">Номер скидки</param>
    /// <param name="data">Данные редактируемой скидки</param>
    /// <returns>204</returns>
    [Authorize(Roles = "administrator")]
    [HttpPatch("{id:int}")]
    public ActionResult<DetailedActiveDiscountWrapper> Update(int id, [FromBody] DetailedActiveDiscountWrapper data) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            _activeDiscountService.Update(id, data);
            return NoContent();
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Деактивация скидки. Только под администратором
    /// </summary>
    /// <param name="id">Номер деактивируемой скидки</param>
    /// <returns>204</returns>
    [Authorize(Roles = "administrator")]
    [HttpDelete("{id:int}/deactivate")]
    public IActionResult Deactivate(int id) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            _activeDiscountService.Deactivate(id);
            return NoContent();
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
}