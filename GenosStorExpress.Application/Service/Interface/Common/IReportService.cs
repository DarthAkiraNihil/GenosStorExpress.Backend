namespace GenosStorExpress.Application.Service.Interface.Common {
    /// <summary>
    /// Интерфейс сервиса отчётов
    /// </summary>
    public interface IReportService {
        /// <summary>
        /// Генерация чека для заказа
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="orderId">Номер заказа</param>
        /// <returns></returns>
        MemoryStream CreateOrderReceipt(string customerId, int orderId);
        /// <summary>
        /// Генерация счёта-фактуры для заказа
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="orderId">Номер заказа</param>
        /// <returns></returns>
        MemoryStream CreateOrderInvoice(string customerId, int orderId);
        /// <summary>
        /// Генерация истории заказов
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        MemoryStream CreateOrderHistoryReport(string customerId);
        /// <summary>
        /// Генерация отчёта по продажам
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <returns></returns>
        MemoryStream GenerateSalesAnalysisReport(string sudoId, DateTime startDate, DateTime endDate);
    }
}