using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Application.Service.Implementation.Base;

/// <summary>
/// Абстрактный класс, предоставляющий методы, общие для всех сервисов товаров
/// </summary>
public abstract class AbstractItemService {
    private readonly IItemTypeService _itemTypeService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="itemTypeService">Сервис типов товаров</param>
    protected AbstractItemService(IItemTypeService itemTypeService) {
        _itemTypeService = itemTypeService;
    }

    /// <summary>
    /// Установка свойств сущности, общих с обёрткой
    /// </summary>
    /// <param name="entity">Обёрнутая сущность, свойства которой устанавливаются</param>
    /// <param name="wrapper">Обёртка сущности</param>
    /// <exception cref="NullReferenceException">Если в обёртке указан несуществующий тип товара</exception>
    protected void _setEntityPropertiesFromWrapper(Item entity, ItemWrapper wrapper) {
        entity.Name = wrapper.Name;
        entity.Model = wrapper.Model;
        entity.Price = wrapper.Price;
        entity.ImageBase64 = "";
        entity.Description = wrapper.Description;
        
        var itemType = _itemTypeService.GetEntityFromString(wrapper.ItemType);
        if (itemType == null) {
            throw new NullReferenceException($"Типа товара {wrapper.ItemType} не существует");
        }
        
        entity.ItemType = itemType;
    }

    /// <summary>
    /// Установка свойств обёртки, общих с сущностью
    /// </summary>
    /// <param name="entity">Оборачиваемая сущность</param>
    /// <param name="wrapper">Обёртка, свойства которой устанавливаются</param>
    protected void _setWrapperPropertiesFromEntity(Item entity, ItemWrapper wrapper) {
        wrapper.Id = entity.Id;
        wrapper.Name = entity.Name;
        wrapper.Model = entity.Model;
        wrapper.Price = entity.Price;
        wrapper.Description = entity.Description;
        wrapper.ItemType = entity.ItemType.Name;
        wrapper.ReviewsCount = entity.Reviews.Count;
        wrapper.OverallRating = _getOverallRating(entity);
        if (entity.ActiveDiscount != null) {
            wrapper.ActiveDiscount = new ActiveDiscountWrapper {
                Id = entity.ActiveDiscount.Id,
                Value = entity.ActiveDiscount.Value,
                EndsAt = entity.ActiveDiscount.EndsAt
            };
        }
    }

    private double _getOverallRating(Item item) {
        if (item.Reviews.Count == 0) {
            return 0;
        }
        return item.Reviews.Average(r => r.Rating);
    }
}