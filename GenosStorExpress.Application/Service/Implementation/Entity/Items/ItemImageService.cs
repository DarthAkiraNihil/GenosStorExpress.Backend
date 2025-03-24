using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items;

/// <summary>
/// Реализация сервиса, возвращающего изображение предмета
/// </summary>
public class ItemImageService: IItemImageService {
    private readonly IAllItemsRepository _repository;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="repositories">Репозитории проекта</param>
    public ItemImageService(IGenosStorExpressRepositories repositories) {
        _repository = repositories.Items.All;
    }

    
    
    /// <summary>
    /// Получение изображение предмета
    /// </summary>
    /// <param name="itemId">Номер предмета</param>
    /// <returns>Поток байтов, представляющий изображение предмета</returns>
    /// <exception cref="NullReferenceException">Если указанного товара не существует</exception>
    public MemoryStream GetImage(int itemId) {
        
        Item? item = _repository.Get(itemId);
        if (item is null) {
            throw new NullReferenceException($"Товара с номером {itemId} не существует");
        }
        
        return new MemoryStream(Convert.FromBase64String(item.ImageBase64));
    }
}