namespace GenosStorExpress.Application.Service.Interface.Entity.Items;

/// <summary>
/// Интерфейс для сервиса, возвращающего изображение предмета
/// </summary>
public interface IItemImageService {
    /// <summary>
    /// Получение изображение предмета
    /// </summary>
    /// <param name="itemId">Номер предмета</param>
    /// <returns>Поток байтов, представляющий изображение предмета</returns>
    public MemoryStream GetImage(int itemId);
}