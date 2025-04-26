using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Application.Wrappers.Entity.Users;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GenosStorExpress.API.Controllers;

/// <summary>
/// Контроллер <c>LegalEntitiesController</c> предоставляет административные методы для управления юридическими лицами 
/// </summary>
[ApiController]
[Route("api/legal_entities")]
public class LegalEntitiesController: AbstractController {
    private readonly ILegalEntityService _legalEntityService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="legalEntityService">Сервис юридических лиц</param>
    public LegalEntitiesController(UserManager<User> userManager, ILegalEntityService legalEntityService) : base(userManager) {
        _legalEntityService = legalEntityService;
    }
    
    /// <summary>
    /// Подтверждение юридического лица. Только под администратором
    /// </summary>
    /// <param name="legalEntityId">Номер юридического лица</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpPost("{legalEntityId:guid}/verify")]
    public IActionResult Verify(string legalEntityId) {
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }
            
        try {
            _legalEntityService.Verify(user.Id, legalEntityId);
            return NoContent();
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
            
    }
    
    /// <summary>
    /// Отзыв верификации юридического лица. Только под администратором
    /// </summary>
    /// <param name="legalEntityId">Номер юридического лица</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpPost("{legalEntityId:guid}/revoke")]
    public IActionResult Revoke(string legalEntityId) {
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }
            
        try {
            _legalEntityService.Revoke(user.Id, legalEntityId);
            return NoContent();
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
            
    }
    
    /// <summary>
    /// Получение списка подтверждённых юридических лиц. Только под администратором
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpGet("verified")]
    public ActionResult<PaginatedLegalEntityWrapper> GetVerified([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {
            
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }
            
        try {
            return Ok(_legalEntityService.GetVerified(user.Id, pageNumber, pageSize));
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
            
    }
    
    /// <summary>
    /// Получение списка ожидающих подтверждения юридических лиц. Только под администратором
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpGet("awaiting")]
    public ActionResult<PaginatedLegalEntityWrapper> GetAwaiting([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10) {
            
        User? user = _getCurrentUser();
        if (user is null) {
            return Unauthorized(new DetailObject("Доступ запрещён"));
        }
            
        try {
            return Ok(_legalEntityService.GetWaitingForVerification(user.Id, pageNumber, pageSize));
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
            
    }
}