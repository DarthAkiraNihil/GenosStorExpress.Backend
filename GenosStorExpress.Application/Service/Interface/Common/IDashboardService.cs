using GenosStorExpress.Application.Wrappers.Dashboard;

namespace GenosStorExpress.Application.Service.Interface.Common {
    /// <summary>
    /// Интерфейс для сервиса дэшборда
    /// </summary>
    public interface IDashboardService {
        /// <summary>
        /// Получение информации дэшборда. Только под администратором
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <returns>Информацию дэшборда</returns>
        DashboardInfoWrapper GetDashboardInfo(string sudoId);
    }
}