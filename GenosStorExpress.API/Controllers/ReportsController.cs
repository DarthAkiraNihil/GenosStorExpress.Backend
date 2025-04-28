using System.Globalization;
using System.Reflection;
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
    /// <param name="logger">Логгер</param>
    /// <param name="reportService">Сервис отчётов</param>
    public ReportsController(UserManager<User> userManager, ILogger<ReportsController> logger, IReportService reportService) : base(userManager, logger) {
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

        IList<string> parameters = new List<string> {
            $"orderId = {orderId}",
        };
        
        User? user = _getCurrentUser();
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            var result = new FileStreamResult(
                _reportService.CreateOrderReceipt(user.Id, orderId),
                "application/pdf"
            ) {
                FileDownloadName = $"receipt_for_order_{orderId}_for_user_{user.Id}.pdf"
            };
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return result;
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
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

        IList<string> parameters = new List<string> {
            $"orderId = {orderId}",
        };
        
        User? user = _getCurrentUser();
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        try {
            var result = new FileStreamResult(
                _reportService.CreateOrderInvoice(user.Id, orderId),
                "application/pdf"
            ) {
                FileDownloadName = $"invoice_for_order_{orderId}_for_user_{user.Id}.pdf"
            };
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return result;
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
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

        IList<string> parameters = new List<string> {
            "<no parameters>",
        };
        
        User? user = _getCurrentUser();
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }

        
        try {
            var result = new FileStreamResult(
                _reportService.CreateOrderHistoryReport(user.Id),
                "application/pdf"
            ) {
                FileDownloadName = $"order_history_for_user_{user.Id}.pdf"
            };
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return result;
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
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

        IList<string> parameters = new List<string> {
            $"startDate = {startDate}",
            $"endDate = {endDate}",
        };
        
        User? user = _getCurrentUser();
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access denied", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        
        try {
            
            string format = "dd.MM.yyyy";
            
            var result = new FileStreamResult(
                _reportService.GenerateSalesAnalysisReport(
                    user.Id, 
                    DateTime.ParseExact(startDate, format, CultureInfo.InvariantCulture), 
                    DateTime.ParseExact(endDate, format, CultureInfo.InvariantCulture)
                ),
                "application/pdf"
            ) {
                FileDownloadName = $"sales_report_{startDate}-{endDate}.pdf"
            };
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "", _getCurrentUser());
            return result;
        } catch (NullReferenceException e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject(e.Message));
        } catch (Exception e) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
            return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
        }
    }

}