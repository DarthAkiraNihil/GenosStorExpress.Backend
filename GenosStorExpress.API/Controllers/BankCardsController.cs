using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GenosStorExpress.API.Controllers;

/// <summary>
/// Контроллер <c>BankCardsController</c> предоставляет стандартные методы для управления банковскими картами
/// </summary>
[ApiController]
[Route("api/bank_cards")]
public class BankCardsController: AbstractController {
    
    private readonly IBankCardService _bankCardService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="bankCardService">Сервис банковских карт</param>
    public BankCardsController(UserManager<User> userManager, IBankCardService bankCardService) : base(userManager) {
        _bankCardService = bankCardService;
    }
    
    [HttpGet("{type}")]
    public ActionResult<PaginatedAnonymousItemWrapper> List(string type, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10, [FromQuery] string? filters = null) {
        
        ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);

        if (descriptor == ItemTypeDescriptor.Unknown) {
            return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
        }

        try {
            PaginatedAnonymousItemWrapper result;
            if (filters != null) {
                var deserializedFilters = JsonSerializer.Deserialize<IDictionary<string, dynamic>>(filters);
                if (deserializedFilters == null) {
                    return BadRequest(new DetailObject("Не получилось прочитать содержимое фильтров"));
                }
                result = _itemServiceRouter.Filter(descriptor, deserializedFilters, pageNumber, pageSize);
            } else {
                result = _itemServiceRouter.List(descriptor, pageNumber, pageSize);
            }

            if (result.Items.Count == 0) {
                result.Previous = null;
                result.Next = null;
            }
            
            return Ok(result);
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
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
        
        ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);

        if (descriptor == ItemTypeDescriptor.Unknown) {
            return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
        }

        try {
            return Ok(_itemServiceRouter.FilterData(descriptor));
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
            item.IsInCart = _isInCart(item.Id);
            
            User? user = _getCurrentUser();
            if (user is not null) {
                item.LeftReview = _itemsService.GetReview(item.Id, user.Id);
            }
            
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
}