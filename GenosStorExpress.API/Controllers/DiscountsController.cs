
using System.Reflection;
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
public class DiscountsController : AbstractController {
    
    private readonly IActiveDiscountService _activeDiscountService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="logger">Логгер</param>
    /// <param name="activeDiscountService">Сервис скидок</param>
    public DiscountsController(UserManager<User> userManager, ILogger<DiscountsController> logger, IActiveDiscountService activeDiscountService) : base(userManager, logger) {
        _activeDiscountService = activeDiscountService;
    }

    /// <summary>
    /// Создание скидки на товар. Только под администратором
    /// </summary>
    /// <param name="itemId">Номер товара</param>
    /// <param name="data">Данные создаваемой скидки</param>
    /// <returns>Созданную скидку</returns>
    [Authorize(Roles = "administrator")]
    [HttpPost("{itemId:int}")]
    public IActionResult Activate(int itemId, [FromBody] ActiveDiscountWrapper data) {

        IList<string> parameters = new List<string> {
            $"itemId = {itemId}",
            $"data = {data}",
        };
        
        User? user = _getCurrentUser();
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            _activeDiscountService.Activate(itemId, data);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return Ok(data);
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Редактирование скидки. Только под администратором
    /// </summary>
    /// <param name="discountId">Номер скидки</param>
    /// <param name="data">Данные редактируемой скидки</param>
    /// <returns>204</returns>
    [Authorize(Roles = "administrator")]
    [HttpPut("{discountId:int}")]
    public IActionResult Edit(int discountId, [FromBody] ActiveDiscountWrapper data) {

        IList<string> parameters = new List<string> {
            $"discountId = {discountId}",
            $"data = {data}",
        };
        
        User? user = _getCurrentUser();
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            _activeDiscountService.Edit(discountId, data);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return NoContent();
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
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

        IList<string> parameters = new List<string> {
            $"id = {id}",
        };
        
        User? user = _getCurrentUser();
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            _activeDiscountService.Deactivate(id);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return NoContent();
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
}