using GenosStorExpress.Domain.Entity.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics {
    
    /// <summary>
    /// Интерфейс сервиса режимов работы DPI
    /// </summary>
    public interface IDPIModeService {
        /// <summary>
        /// Получение списка доступных режимов работы DPI
        /// </summary>
        /// <returns>Список доступных режимов работы DPI</returns>
        List<int> List();
        /// <summary>
        /// Получение сущности DPI по её значению
        /// </summary>
        /// <param name="dpi">Значение DPI</param>
        /// <returns>Сущность DPI</returns>
        DPIMode? GetByValue(int dpi);
    }
}