using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenosStorExpress.API.Controllers {
    /// <summary>
    /// Контроллер <c>ItemsController</c> предоставляет методы для работы с товарами.
    /// Важно отметить, что некоторые операций может выполнить только администротор.
    /// </summary>
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase {
        
        private readonly IItemServiceRouter _itemServiceRouter;
        private readonly IItemTypeService _itemTypeService;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemServiceRouter">Маршрутизатор сервисов товаров по типам</param>
        /// <param name="itemTypeService">Сервис типов товаров</param>
        public ItemsController(IItemServiceRouter itemServiceRouter, IItemTypeService itemTypeService) {
            _itemServiceRouter = itemServiceRouter;
            _itemTypeService = itemTypeService;
        }
        
        /// <summary>
        /// Получение списка товаров определённой категории 
        /// </summary>
        /// <param name="type">Тип товара. Допустимые значения</param>
        /// <returns>Список товаров с основной информацией</returns>
        [HttpGet("{type}")]
        public ActionResult<IEnumerable<ItemWrapper>> List(string type) {
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);

            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
            }

            try {
                IEnumerable<ItemWrapper> list = _itemServiceRouter.List(descriptor).Select(
                    i => new ItemWrapper {
                        Name = i.Name,
                        Model = i.Model,
                        Description = i.Description,
                        Price = i.Price,
                        Id = i.Id,
                        ItemType = i.ItemType
                    });
                return Ok(list);
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
            } catch (Exception e) {
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
        [Authorize(Roles = "administrator")]
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
        [Authorize(Roles = "administrator")]
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
    }
}
