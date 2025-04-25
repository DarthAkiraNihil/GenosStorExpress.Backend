namespace GenosStorExpress.Application.Service.Interface.Common {
    /// <summary>
    /// Интерфейс стандартный сервисов
    /// </summary>
    public interface ICommonServices {
        /// <summary>
        /// Сервис оплаты
        /// </summary>
	    IPaymentService Payment { get; }
        /// <summary>
        /// Сервис отчётов
        /// </summary>
        IReportService Reports { get; }
        /// <summary>
        /// Сервис дэшборда
        /// </summary>
        IDashboardService Dashboard { get; }
    }
}