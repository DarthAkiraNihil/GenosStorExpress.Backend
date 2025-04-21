using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity;
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
    /// <param name="customerId">Номер покупателя</param>
    /// <param name="review">Отзыв</param>
    /// <exception cref="NullReferenceException">Если указанного товара не существует</exception>
    public void LeaveReview(int itemId, string customerId, ReviewWrapper review) {
        Item? obj =  _items.Get(itemId);

        if (obj == null) {
            throw new NullReferenceException($"Товара с номером {itemId} не существует");
        }

        var left = new Review {
            Rating = review.Rating,
            Comment = review.Comment,
            CustomerId = customerId,
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
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns>Список отзывов на товар</returns>
    /// <exception cref="NullReferenceException">Если указанного товара не существует</exception>
    public PaginatedReviewWrapper GetReviews(int itemId, int pageNumber, int pageSize) {
        Item? obj =  _items.Get(itemId);

        if (obj == null) {
            throw new NullReferenceException($"Товара с номером {itemId} не существует");
        }

        var reviews = obj.Reviews;
        
        return new PaginatedReviewWrapper {
            Count = reviews.Count,
            Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
            Next = (pageNumber + 1) * pageSize >= reviews.Count ? null : (pageNumber + 1).ToString(),
            Items = reviews.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                r => new ReviewWrapper {
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Author = r.Customer!.Email!
                }
            ).ToList()
        };
        
    }

    /// <summary>
    /// Получение отзыва на товар от конкретного покупателя
    /// </summary>
    /// <param name="itemId">Номер товара</param>
    /// <param name="customerId">Номер покупателя</param>
    /// <returns></returns>
    public ReviewWrapper? GetReview(int itemId, string customerId) {
        Item? obj =  _items.Get(itemId);

        if (obj == null) {
            throw new NullReferenceException($"Товара с номером {itemId} не существует");
        }
        
        var review = obj.Reviews.FirstOrDefault(r => r.CustomerId == customerId);
        if (review != null) {
            return new ReviewWrapper {
                Rating = review.Rating,
                Comment = review.Comment,
                Author = review.Customer!.Email!
            };
        }

        return null;
    }

    public void SetImage(string sudoId, int itemId, MemoryStream stream) {
        throw new NotImplementedException();
    }
}
