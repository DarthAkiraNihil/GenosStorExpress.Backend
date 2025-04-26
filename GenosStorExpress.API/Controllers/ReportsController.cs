using System.Globalization;
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
                FileDownloadName = $"receipt_for_order_{orderId}_for_user_{user.Id}.pdf"
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
                FileDownloadName = $"invoice_for_order_{orderId}_for_user_{user.Id}.pdf"
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
                FileDownloadName = $"order_history_for_user_{user.Id}.pdf"
            };
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

    /// <summary>
    /// Генерация отчёта по продажам. Только под администратором
    /// </summary>
    /// <param name="startDate">Дата начала периода продаж</param>
    /// <param name="endDate">Дата окончания периода продаж</param>
    /// <returns></returns>
    [Authorize(Roles = "administrator")]
    [HttpGet("sales_report")]
    public IActionResult GenerateSalesReport([FromQuery] string startDate, [FromQuery] string endDate) {
        User? user = _getCurrentUser();
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            
            string format = "dd.MM.yyyy";
            
            
            return new FileStreamResult(
                _reportService.GenerateSalesAnalysisReport(
                    user.Id, 
                    DateTime.ParseExact(startDate, format, CultureInfo.InvariantCulture), 
                    DateTime.ParseExact(endDate, format, CultureInfo.InvariantCulture)
                    ),
                "application/pdf"
            ) {
                FileDownloadName = $"sales_report_{startDate}-{endDate}.pdf"
            };
        } catch (NullReferenceException e) {
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

}