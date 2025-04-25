using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GenosStorExpress.API.Controllers;

/// <summary>
/// Контроллер <c>ReportsController</c> предоставляет методы для получения чеков и всевозможных отчётов
/// </summary>
[Route("api/reports")]
[ApiController]
public class ReportsController: AbstractController {
    
    private readonly IReportService _reportService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователей</param>
    /// <param name="reportService">Сервис отчётов</param>
    public ReportsController(UserManager<User> userManager, IReportService reportService) : base(userManager) {
        _reportService = reportService;
    }


    /// <summary>
    /// Генерация чека для заказа. Только для физических лиц
    /// </summary>
    /// <param name="orderId">Номер заказа</param>
    /// <returns></returns>
    [Authorize(Roles = "individual_entity")]
    [HttpGet("receipt/{orderId:int}")]
    public IActionResult GenerateReceipt(int orderId) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            return new FileStreamResult(
                _reportService.CreateOrderReceipt(user.Id, orderId),
                "application/pdf"
            ) {
                FileDownloadName = "receipt.pdf"
            };
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
    /// <summary>
    /// Генерация счёта-фактура для заказа. Только для юридических лиц
    /// </summary>
    /// <param name="orderId">Номер заказа</param>
    /// <returns></returns>
    [Authorize(Roles = "legal_entity")]
    [HttpGet("invoice/{orderId:int}")]
    public IActionResult GenerateInvoice(int orderId) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            return new FileStreamResult(
                _reportService.CreateOrderInvoice(user.Id, orderId),
                "application/pdf"
            ) {
                FileDownloadName = "invoice.pdf"
            };
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
    /// <summary>
    /// Генерация истории заказов
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "individual_entity,legal_entity")]
    [HttpGet("order_history")]
    public IActionResult GenerateOrderHistory() {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        
        try {
            return new FileStreamResult(
                _reportService.CreateOrderHistoryReport(user.Id),
                "application/pdf"
            ) {
                FileDownloadName = "order_history.pdf"
            };
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }
    
}