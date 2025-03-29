using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items;

/// <summary>
/// Реализация общего сервиса товаров
/// </summary>
public class AllItemsService: AbstractItemService, IAllItemsService {
    private readonly IGenosStorExpressRepositories _repositories;
    private readonly IAllItemsRepository _items;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="itemTypeService">Сервис типов товаров</param>
    /// <param name="repositories">Репозитории проекта</param>
    public AllItemsService(IItemTypeService itemTypeService, IGenosStorExpressRepositories repositories) : base(itemTypeService) {
        _repositories = repositories;
        _items = _repositories.Items.All;
    }
    
    /// <summary>
    /// Получение краткой информации о товаре в обёртке 
    /// </summary>
    /// <param name="id">Номер товара</param>
    /// <returns>Обёртку краткой информации о товаре или null, если товар не найден</returns>
    public ItemWrapper? Get(int id) {
        Item? obj =  _items.Get(id);

        if (obj == null) {
            return null;
        }
        
        var wrapped = new ItemWrapper();
        _setWrapperPropertiesFromEntity(obj, wrapped);
        return wrapped;
    }
    
    /// <summary>
    /// Получение списка основной информации о товарах
    /// </summary>
    /// <returns>Список основной информации о товарах</returns>
    public List<ItemWrapper> List() {
        return _items.List().Select(
            obj => {
                var wrapped = new ItemWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }
        ).ToList();
    }

    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="id">Номер удаляемого товара</param>
    public void Delete(int id) {
        _repositories.Items.All.Delete(id);
    }

    /// <summary>
    /// Сохранение изменений
    /// </summary>
    /// <returns>Количество изменений</returns>
    public int Save() {
        return _repositories.Save();
    }
    
    /// <summary>
    /// Оставление отзыва на товар
    /// </summary>
    /// <param name="itemId">Номер товара</param>
    /// <param name="review">Отзыв</param>
    /// <exception cref="NullReferenceException">Если указанного товара не существует</exception>
    public void LeaveReview(int itemId, ReviewWrapper review) {
        Item? obj =  _items.Get(itemId);

        if (obj == null) {
            throw new NullReferenceException($"Товара с номером {itemId} не существует");
        }

        var left = new Review {
            Rating = review.Rating,
            Comment = review.Comment,
            Item = obj
        };
        
        obj.Reviews.Add(left);
        _items.Update(obj);
        _repositories.Save();
    }

    /// <summary>
    /// Получение отзывов на товар
    /// </summary>
    /// <param name="itemId">Номер товара</param>
    /// <returns>Список отзывов на товар</returns>
    /// <exception cref="NullReferenceException">Если указанного товара не существует</exception>
    public IList<ReviewWrapper> GetReviews(int itemId) {
        Item? obj =  _items.Get(itemId);

        if (obj == null) {
            throw new NullReferenceException($"Товара с номером {itemId} не существует");
        }
        
        return obj.Reviews.Select(
            r => new ReviewWrapper {
                Rating = r.Rating,
                Comment = r.Comment,
            }).ToList();
    }

    public void SetImage(string sudoId, int itemId, MemoryStream stream) {
        throw new NotImplementedException();
    }
}
