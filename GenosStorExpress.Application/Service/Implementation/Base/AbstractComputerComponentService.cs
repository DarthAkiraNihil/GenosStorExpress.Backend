using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

/// <summary>
/// Абстрактный класс, предоставляющий методы, общие для всех сервисов компьютерных комплектующих
/// </summary>
public abstract class AbstractComputerComponentService: AbstractItemService {
    private readonly IVendorService _vendorService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="itemTypeService">Сервис типов товаров</param>
    /// <param name="activeDiscountService">Сервис скидок</param>
    /// <param name="vendorService">Сервис производителей</param>
    protected AbstractComputerComponentService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService) : base(itemTypeService, activeDiscountService) {
        _vendorService = vendorService;
    }

    /// <summary>
    /// Установка свойств сущности, общих с обёрткой
    /// </summary>
    /// <param name="entity">Обёрнутая сущность, свойства которой устанавливаются</param>
    /// <param name="wrapper">Обёртка сущности</param>
    /// <exception cref="NullReferenceException">Если в обёртке указан несуществующий тип товара или производитель</exception>
    protected void _setEntityPropertiesFromWrapper(ComputerComponent entity, ComputerComponentWrapper wrapper) {
        base._setEntityPropertiesFromWrapper(entity, wrapper);
        entity.TDP = wrapper.TDP;
        
        var vendor = _vendorService.GetEntityFromString(wrapper.Vendor);
        if (vendor == null) {
            throw new NullReferenceException($"Производителя {wrapper.Vendor} не существует");
        }

        entity.Vendor = vendor;
    }
    
    /// <summary>
    /// Установка свойств обёртки, общих с сущностью
    /// </summary>
    /// <param name="entity">Оборачиваемая сущность</param>
    /// <param name="wrapper">Обёртка, свойства которой устанавливаются</param>
    protected void _setWrapperPropertiesFromEntity(ComputerComponent entity, ComputerComponentWrapper wrapper) {
        base._setWrapperPropertiesFromEntity(entity, wrapper);
        wrapper.TDP = entity.TDP;
        wrapper.Vendor = entity.Vendor.Name;
    }
}