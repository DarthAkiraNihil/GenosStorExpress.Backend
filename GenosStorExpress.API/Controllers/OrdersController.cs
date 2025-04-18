using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GenosStorExpress.API.Controllers;

/// <summary>
/// Контроллер <c>OrdersController</c> предоставляет методы для управления заказами
/// </summary>
[Route("api/orders")]
[ApiController]
public class OrdersController: AbstractController {
    
    private readonly IOrderService _orderService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="orderService">Сервис заказов</param>
    public OrdersController(UserManager<User> userManager, IOrderService orderService) : base(userManager) {
        _orderService = orderService;
    }

    /// <summary>
    /// Получение деталей заказа
    /// </summary>
    /// <param name="id">Номер заказа</param>
    /// <returns>Подробную информацию о заказе</returns>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpGet("{id:int}")]
    public ActionResult<OrderWrapper> GetDetails(int id) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            OrderWrapper? order = _orderService.Get(id, user.Id);
            if (order is null) {
                return BadRequest(new DetailObject($"Заказа с номером {id} не существует"));
            }

            return order;
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Получение списка заказов текущего пользователя
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns>Список заказов текущего пользователя</returns>
    [Authorize(Roles = "individual_entity,legal_entity")]
    [HttpGet]
    public ActionResult<PaginatedShortOrderInfoWrapper> GetList([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            PaginatedShortOrderInfoWrapper orders = _orderService.List(user.Id, pageNumber, pageSize);
            return Ok(orders);
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
    /// <summary>
    /// Создание заказа (из корзины)
    /// </summary>
    /// <returns>Краткая информация о созданном заказе</returns>
    [Authorize(Roles = "individual_entity,legal_entity")]
    [HttpPost]
    public ActionResult<ShortOrderWrapper> CreateOrder() {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            ShortOrderWrapper created = _orderService.CreateOrderFromCart(user.Id);
            return created;
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
}