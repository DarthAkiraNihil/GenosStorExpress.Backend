using System.Reflection;
using System.Text.Json;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace GenosStorExpress.API.Controllers {
    /// <summary>
    /// Контроллер <c>ItemsController</c> предоставляет методы для работы с товарами.
    /// Важно отметить, что некоторые операций может выполнить только администротор.
    /// </summary>
    [Route("api/items")]
    [ApiController]
    public class ItemsController : AbstractController {
        
        private readonly IItemServiceRouter _itemServiceRouter;
        private readonly IItemTypeService _itemTypeService;
        private readonly IItemImageService _itemImageService;
        private readonly IAllItemsService _itemsService;
        private readonly ICartService _cartService;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemServiceRouter">Маршрутизатор сервисов товаров по типам</param>
        /// <param name="logger">Логгер</param>
        /// <param name="itemTypeService">Сервис типов товаров</param>
        /// <param name="itemImageService">Сервис изображений товаров</param>
        /// <param name="itemsService">Общий сервис товаров</param>
        /// <param name="userManager">Менеджер пользователей</param>
        /// <param name="cartService">Сервис корзин</param>
        public ItemsController(UserManager<User> userManager, ILogger<ItemsController> logger, IItemServiceRouter itemServiceRouter, IItemTypeService itemTypeService, IItemImageService itemImageService, IAllItemsService itemsService, ICartService cartService) : base(userManager, logger) {
            _itemServiceRouter = itemServiceRouter;
            _itemTypeService = itemTypeService;
            _itemImageService = itemImageService;
            _itemsService = itemsService;
            _cartService = cartService;
        }

        /// <summary>
        /// Получение списка товаров определённой категории 
        /// </summary>
        /// <param name="type">Тип товара. Допустимые значения</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="filters">Фильтры, применяемые к списку товаров</param>
        /// <returns>Список товаров с основной информацией</returns>
        [HttpGet("{type}")]
        public ActionResult<PaginatedAnonymousItemWrapper> List(string type, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10, [FromQuery] string? filters = null) {

            IList<string> parameters = new List<string> {
                $"type = {type}",
                $"pageNumber = {pageNumber}",
                $"pageSize = {pageSize}",
                $"filters = {filters}",
            };
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);

            if (descriptor == ItemTypeDescriptor.Unknown) {
                var msg = $"Неизвестный тип товара - {type}";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }

            try {
                PaginatedAnonymousItemWrapper result;
                if (filters != null) {
                    var deserializedFilters = JsonSerializer.Deserialize<IDictionary<string, dynamic>>(filters);
                    if (deserializedFilters == null) {
                        var msg = "Не получилось прочитать содержимое фильтров";
                        _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                        return BadRequest(new DetailObject(msg));
                    }
                    result = _itemServiceRouter.Filter(descriptor, deserializedFilters, pageNumber, pageSize);
                } else {
                    result = _itemServiceRouter.List(descriptor, pageNumber, pageSize);
                }

                if (result.Items.Count == 0) {
                    result.Previous = null;
                    result.Next = null;
                }
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
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
        /// Получение информации о возможных фильтрах для данного типа товара
        /// </summary>
        /// <param name="type">Дескриптор типа товаров</param>
        /// <returns></returns>
        [HttpGet("{type}/filter_data")]
        public ActionResult<IList<FilterDescription>> FilterData(string type) {

            IList<string> parameters = new List<string> {
                $"type = {type}"
            };
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);

            if (descriptor == ItemTypeDescriptor.Unknown) {
                var msg = $"Неизвестный тип товара - {type}";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }

            try {
                var result = _itemServiceRouter.FilterData(descriptor);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
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
        /// Получение информации о конкретном товаре
        /// </summary>
        /// <param name="type">Тип товара. Допустимые значения</param>
        /// <param name="id">Номер товара</param>
        /// <returns>Подробную информаци
        /// ю о товаре со всеми характеристиками</returns>
        [HttpGet("{type}/{id:int}")]
        public ActionResult<AnonymousItemWrapper> Get(string type, int id) {

            IList<string> parameters = new List<string> {
                $"type = {type}",
                $"id = {id}"
            };
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                var msg = $"Неизвестный тип товара - {type}";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }

            try {
                AnonymousItemWrapper item = _itemServiceRouter.Get(descriptor, id);
                item.IsInCart = _isInCart(item.Id);
                User? user = _getCurrentUser();
                if (user is not null) {
                    item.LeftReview = _itemsService.GetReview(item.Id, user.Id);
                }
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
                return Ok(item);
            } catch (NullReferenceException e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }

        }
        
        /// <summary>
        /// Создание товара. Только под администратором
        /// </summary>
        /// <param name="value">Данные о создаваемом товаре</param>
        /// <returns>Информацию о созданном товаре</returns>
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public ActionResult<AnonymousItemWrapper> Create([FromBody]AnonymousItemWrapper value) {

            IList<string> parameters = new List<string> {
                $"value = {value}",
            };
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(value.ItemType);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                var msg = $"Неизвестный тип товара - {value.ItemType}";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }

            try {
                _itemServiceRouter.Create(descriptor, value);
                _itemServiceRouter.Save(descriptor);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
                return CreatedAtAction(nameof(Create), new { id = value.Id }, value);
            } catch (NullReferenceException e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
            
        }
        
        /// <summary>
        /// Обновление информации о товаре. Только под администратором
        /// </summary>
        /// <param name="id">Номер товара</param>
        /// <param name="value">Обновлённая информация о товаре</param>
        /// <returns>204 в случае успеха</returns>
        [Authorize(Roles = "administrator")]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody]AnonymousItemWrapper value) {

            IList<string> parameters = new List<string> {
                $"value = {value}",
            };
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(value.ItemType);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                var msg = $"Неизвестный тип товара - {value.ItemType}";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }

            if (id != value.Id) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Ошибка - попытка изменить номер товара", _getCurrentUser(), true);
                return BadRequest(new DetailObject("Ошибка - попытка изменить номер товара"));
            }

            try {
                _itemServiceRouter.Update(descriptor, id, value);
                _itemServiceRouter.Save(descriptor);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
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
        /// Удаление товара. Только под администратором
        /// </summary>
        /// <param name="type">Тип товара. Допустимые значения</param>
        /// <param name="id">Номер товара</param>
        /// <returns>204 в случае успеха</returns>
        [Authorize(Roles = "administrator")]
        [HttpDelete("{type}/{id:int}")]
        public IActionResult Delete(string type, int id) {

            IList<string> parameters = new List<string> {
                $"type = {type}",
                $"id = {id}",
            };
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                var msg = $"Неизвестный тип товара - {type}";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }
            
            try {
                _itemServiceRouter.Delete(descriptor, id);
                _itemServiceRouter.Save(descriptor);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
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
        /// Получение изображения товара
        /// </summary>
        /// <param name="id">Номер товара</param>
        /// <returns>Изображение товара</returns>
        [HttpGet("{id:int}/image")]
        public IActionResult GetItemImage(int id) {

            IList<string> parameters = new List<string> {
                "id = {id}",
            };
            
            try {
                MemoryStream image = _itemImageService.GetImage(id);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
                return new FileStreamResult(image, "image/png");
            } catch (NullReferenceException e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
        }
        
        /// <summary>
        /// Оставление отзыва на товар
        /// </summary>
        /// <param name="id">Номер товара</param>
        /// <param name="review">Отзыв</param>
        /// <response code="204">Успех</response>
        [Authorize(Roles = "individual_entity,legal_entity")]
        [HttpPost("{id:int}/leave_review")]
        public IActionResult LeaveReview(int id, [FromBody]ReviewWrapper review) {

            IList<string> parameters = new List<string> {
                $"id = {id}",
                $"review = {review}",
            };
            
            User? user = _getCurrentUser();
            if (user is null) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
                return Unauthorized(new DetailObject("Доступ запрещён"));
            }
            
            try {
                _itemsService.LeaveReview(id, user.Id, review);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
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
        /// Получение отзывов на товар
        /// </summary>
        /// <param name="id">Номер товара</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список отзывов</returns>
        /// <response code="200">Успех</response>
        [HttpGet("{id:int}/reviews")]
        public ActionResult<PaginatedReviewWrapper> GetReviews(int id, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {

            IList<string> parameters = new List<string> {
                $"id = {id}",
                $"pageNumber = {pageNumber}",
                $"pageSize = {pageSize}"
            };
            
            try {
                var result = _itemsService.GetReviews(id, pageNumber, pageSize);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
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
        /// Установка изображения товара. Только под администратором.
        /// </summary>
        /// <param name="id">Номер товара</param>
        /// <param name="file">Изображение товара</param>
        /// <returns>204 в случае успеха</returns>
        [Authorize(Roles = "administrator")]
        [HttpGet("{id:int}/set_image")]
        public IActionResult SetImage(int id, IFormFile file) {

            IList<string> parameters = new List<string> {
                $"id = {id}",
                $"file = {file}",
            };
            
            User? user = _getCurrentUser();
            if (user is null) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
                return Unauthorized(new DetailObject("Доступ запрещён"));
            }

            try {
                var stream = new MemoryStream();
                file.CopyTo(stream);
                _itemsService.SetImage(user.Id, id, stream);
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "");
                return NoContent();
            } catch (NullReferenceException e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
        }

        private bool _isInCart(int itemId) {
            User? user = _getCurrentUser();
            if (user == null) {
                return false;
            }

            return _cartService.IsInCart(itemId, user.Id);
        }

    }
}
