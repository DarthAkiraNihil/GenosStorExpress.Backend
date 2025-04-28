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
/// Контроллер <c>CartsController</c> предоставляет стандартные методы для управления корзиной
/// </summary>
[Route("api/cart")]
[ApiController]
public class CartsController: AbstractController {
    
    private readonly ICartService _cartService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="logger">Логгер</param>
    /// <param name="cartService">Сервис корзин</param>
    public CartsController(UserManager<User> userManager, ILogger<CartsController> logger, ICartService cartService) : base(userManager, logger) {
        _cartService = cartService;
    }

    /// <summary>
    /// Добавление товара в корзину
    /// </summary>
    /// <param name="id">Номер добавляемого товара</param>
    /// <response code="200">Успех</response>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpPost("add/{id:int}")]
    public IActionResult AddToCart(int id) {

        IList<string> parameters = new List<string> {
            $"id = {id}",
        };
        
        User? user = _getCurrentUser();
        if (user is null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            _cartService.AddToCart(id, user.Id);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Товар был успешно добавлен в корзину", _getCurrentUser());
            return Ok(new DetailObject("Товар был успешно добавлен в корзину"));
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }

    }

    /// <summary>
    /// Удаление товара из корзины
    /// </summary>
    /// <param name="id">Номер удаляемого товара</param>
    /// <response code="200">Успех</response>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveFromCart(int id) {

        IList<string> parameters = new List<string> {
            $"id = {id}",
        };
        
        User? user = _getCurrentUser();
        if (user is null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            _cartService.RemoveFromCart(id, user.Id);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Товар был успешно удалён из корзины", _getCurrentUser());
            return Ok(new DetailObject("Товар был успешно удалён из корзины"));
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
    /// <summary>
    /// Увеличение количества товара в корзине на 1
    /// </summary>
    /// <param name="id">Номер добавляемого товара</param>
    /// <response code="204">Успех</response>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpPost("inc/{id:int}")]
    public IActionResult IncrementItemQuantity(int id) {

        IList<string> parameters = new List<string> {
            $"id = {id}",
        };
        
        User? user = _getCurrentUser();
        if (user is null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            _cartService.IncrementCartItemQuantity(id, user.Id);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return NoContent();
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Уменьшение количества товара в корзине на 1
    /// </summary>
    /// <param name="id">Номер уменьшаемого товара</param>
    /// <response code="204">Успех</response>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpPost("dec/{id:int}")]
    public IActionResult DecrementItemQuantity(int id) {

        IList<string> parameters = new List<string> {
            $"id = {id}",
        };
        
        User? user = _getCurrentUser();
        if (user is null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            _cartService.DecrementCartItemQuantity(id, user.Id);
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
    /// Получение содержимого корзины текущего пользователя
    /// </summary>
    /// <response code="200">Успех</response>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpGet]
    public ActionResult<PaginatedCartWrapper> GetCart([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {

        IList<string> parameters = new List<string> {
            $"pageNumber = {pageNumber}",
            $"pageSize = {pageSize}",
        };
        
        User? user = _getCurrentUser();
        if (user is null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            var result = _cartService.GetCart(user.Id, pageNumber, pageSize);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return Ok(result);
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    
    /// <summary>
    /// Очистка корзины текущего пользователя
    /// </summary>
    /// <response code="204">Успех</response>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpPost("clear")]
    public IActionResult ClearCart() {

        IList<string> parameters = new List<string> {
            "<no parameters>",
        };
        
        User? user = _getCurrentUser();
        if (user is null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            _cartService.ClearCart(user.Id);
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