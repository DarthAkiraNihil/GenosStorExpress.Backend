using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
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
    /// <param name="logger">Логгер</param>
    public BankCardsController(UserManager<User> userManager, ILogger<BankCardsController> logger, IBankCardService bankCardService) : base(userManager, logger) {
        _bankCardService = bankCardService;
    }
    
    
    /// <summary>
    /// Получение списка банковских карт текущего пользователя
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [Authorize(Roles = "individual_entity,legal_entity")]
    [HttpGet]
    public ActionResult<PaginatedBankCardWrapper> List([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {
        
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            PaginatedBankCardWrapper result = _bankCardService.List(user.Id, pageNumber, pageSize);

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
    /// Создание новой банковской карты
    /// </summary>
    /// <param name="value">Данные создаваемой карты</param>
    /// <returns></returns>
    [Authorize(Roles = "individual_entity,legal_entity")]
    [HttpPost]
    public ActionResult<AnonymousItemWrapper> Create([FromBody]BankCardWrapper value) {
        
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }

        try {
            _bankCardService.Create(user.Id, value);
            _bankCardService.Save();
            return CreatedAtAction(nameof(Create), new { id = value.Id }, value);
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
        
    }
    
    /// <summary>
    /// Обновление данных о карте
    /// </summary>
    /// <param name="id">Номер карты</param>
    /// <param name="value">Обновлённые данные</param>
    /// <returns></returns>
    [Authorize(Roles = "individual_entity,legal_entity")]
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody]BankCardWrapper value) {
        
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }
        
        if (id != value.Id) {
            return BadRequest(new DetailObject("Ошибка - попытка изменить номер товара"));
        }

        try {
            _bankCardService.Update(user.Id, id, value);
            _bankCardService.Save();
            return NoContent();
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
        
    }
    
    /// <summary>
    /// Удаление банковской карты
    /// </summary>
    /// <param name="id">Номер карты</param>
    /// <returns></returns>
    [Authorize(Roles = "individual_entity,legal_entity")]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) {
        
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }
        
        try {
            _bankCardService.Delete(user.Id, id);
            _bankCardService.Save();
            return NoContent();
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
        
    }
}