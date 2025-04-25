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
    public ActionResult<ShortOrderWrapper> GetDetails(int id) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            ShortOrderWrapper? order = _orderService.Get(id, user.Id);
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
    /// Получение списка товаров в заказе
    /// </summary>
    /// <param name="id">Номер заказа</param>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [Authorize(Roles="individual_entity,legal_entity")]
    [HttpGet("{id:int}/items")]
    public ActionResult<PaginatedOrderItemWrapper> GetItems(int id, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            PaginatedOrderItemWrapper? order = _orderService.GetItems(id, user.Id, pageNumber, pageSize);
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
    
    /// <summary>
    /// Получение списка активных заказов. Только под администратором!
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpGet("all")]
    public ActionResult<PaginatedShortOrderInfoWrapper> GetAll([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            PaginatedShortOrderInfoWrapper orders = _orderService.GetActiveOrders(user.Id, pageNumber, pageSize);
            return Ok(orders);
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
    /// <summary>
    /// Получение деталей любого заказа. Только под администратором
    /// </summary>
    /// <param name="id">Номер заказа</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpGet("{id:int}/details_of_any")]
    public ActionResult<ShortOrderWrapper> GetDetailsOfAny(int id) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            ShortOrderWrapper? order = _orderService.GetDetailsOfAny(id, user.Id);
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
    /// Получение списка товаров в заказе любого покупателя. Только под администратором!
    /// </summary>
    /// <param name="id">Номер заказа</param>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpGet("{id:int}/items_of_any")]
    public ActionResult<PaginatedOrderItemWrapper> GetItemsOfAny(int id, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            PaginatedOrderItemWrapper? order = _orderService.GetItemsOfAny(id, user.Id, pageNumber, pageSize);
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
    /// Продвижение заказа по цепочке обработки. Только под администратором
    /// </summary>
    /// <param name="id">Номер заказа</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpPost("{id:int}/promote")]
    public ActionResult<ShortOrderWrapper> PromoteOrder(int id) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            ShortOrderWrapper order = _orderService.PromoteOrder(id, user.Id);

            return order;
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
    
}