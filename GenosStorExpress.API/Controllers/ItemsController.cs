using System.Collections;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
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

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemServiceRouter">Маршрутизатор сервисов товаров по типам</param>
        /// <param name="itemTypeService">Сервис типов товаров</param>
        /// <param name="itemImageService">Сервис изображений товаров</param>
        /// <param name="itemsService">Общий сервис товаров</param>
        /// <param name="userManager">Менеджер пользователей</param>
        public ItemsController(UserManager<User> userManager, IItemServiceRouter itemServiceRouter, IItemTypeService itemTypeService, IItemImageService itemImageService, IAllItemsService itemsService) : base(userManager) {
            _itemServiceRouter = itemServiceRouter;
            _itemTypeService = itemTypeService;
            _itemImageService = itemImageService;
            _itemsService = itemsService;
        }

        /// <summary>
        /// Получение списка товаров определённой категории 
        /// </summary>
        /// <param name="type">Тип товара. Допустимые значения</param>
        /// <returns>Список товаров с основной информацией</returns>
        [HttpGet("{type}")]
        public ActionResult<IEnumerable<ItemWrapper>> List(string type, [FromBody] IDictionary<string, dynamic>? filters = null) {
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);

            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
            }

            try {
                IEnumerable<ItemWrapper> list = null;
                if (filters != null) {
                    list = _itemServiceRouter.Filter(descriptor, filters);
                } else {
                    list = _itemServiceRouter.List(descriptor);
                }
                return Ok(list.Select(
                    i => new ItemWrapper {
                        Name = i.Name,
                        Model = i.Model,
                        Description = i.Description,
                        Price = i.Price,
                        Id = i.Id,
                        ItemType = i.ItemType
                    }));
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
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
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
            }

            try {
                AnonymousItemWrapper item = _itemServiceRouter.Get(descriptor, id);
                return Ok(item);
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            }
            // } catch (Exception e) {
            //     return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            // }

        }
        
        /// <summary>
        /// Создание товара. Только под администратором
        /// </summary>
        /// <param name="value">Данные о создаваемом товаре</param>
        /// <returns>Информацию о созданном товаре</returns>
        //[Authorize(Roles = "administrator")]
        [HttpPost]
        public ActionResult<AnonymousItemWrapper> Create([FromBody]AnonymousItemWrapper value) {
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(value.ItemType);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {value.ItemType}"));
            }

            try {
                _itemServiceRouter.Create(descriptor, value);
                _itemServiceRouter.Save(descriptor);
                return CreatedAtAction(nameof(Create), new { id = value.Id }, value);
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
            
        }
        
        /// <summary>
        /// Обновление информации о товаре. Только под администратором
        /// </summary>
        /// <param name="id">Номер товара</param>
        /// <param name="value">Обновлённая информация о товаре</param>
        /// <returns>204 в случае успеха</returns>
        //[Authorize(Roles = "administrator")]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody]AnonymousItemWrapper value) {
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(value.ItemType);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {value.ItemType}"));
            }

            if (id != value.Id) {
                return BadRequest(new DetailObject("Ошибка - попытка изменить номер товара"));
            }

            try {
                _itemServiceRouter.Update(descriptor, id, value);
                _itemServiceRouter.Save(descriptor);
                return NoContent();
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
            
        }
        
        /// <summary>
        /// Удаление товара. Только под администратором
        /// </summary>
        /// <param name="type">Тип товара. Допустимые значения</param>
        /// <param name="id">Номер товара</param>
        /// <returns>204 в случае успеха</returns>
        // [Authorize(Roles = "administrator")]
        [HttpDelete("{type}/{id:int}")]
        public IActionResult Delete(string type, int id) {
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
            }
            
            try {
                _itemServiceRouter.Delete(descriptor, id);
                _itemServiceRouter.Save(descriptor);
                return NoContent();
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
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
            try {
                MemoryStream image = _itemImageService.GetImage(id);
                return new FileStreamResult(image, "image/png");
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
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
        [HttpGet("{id:int}/leave_review")]
        public IActionResult LeaveReview(int id, [FromBody]ReviewWrapper review) {
            try {
                _itemsService.LeaveReview(id, review);
                return NoContent();
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
        }
        
        /// <summary>
        /// Получение отзывов на товар
        /// </summary>
        /// <param name="id">Номер товара</param>
        /// <returns>Список отзывов</returns>
        /// <response code="200">Успех</response>
        [HttpGet("{id:int}/reviews")]
        public IActionResult GetReviews(int id) {
            try {
                return Ok(_itemsService.GetReviews(id));
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
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
            
            User? user = _getCurrentUser();
            if (user is null) {
                return Unauthorized(new DetailObject("Доступ запрещён"));
            }

            try {
                var stream = new MemoryStream();
                file.CopyTo(stream);
                _itemsService.SetImage(user.Id, id, stream);
                return NoContent();
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
        }

    }
}
