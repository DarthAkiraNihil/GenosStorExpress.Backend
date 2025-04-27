using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

/// <summary>
/// Абстрактный класс, предоставляющий методы, общие для всех сервисов дисковых накопителей
/// </summary>
public abstract class AbstractDiskDriveService: AbstractComputerComponentService {
    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="itemTypeService">Сервис типов товаров</param>
    /// <param name="activeDiscountService">Сервис скидок</param>
    /// <param name="vendorService">Сервис производителей</param>
    protected AbstractDiskDriveService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService) : base(itemTypeService, activeDiscountService, vendorService) {
    }
    
    /// <summary>
    /// Установка свойств сущности, общих с обёрткой
    /// </summary>
    /// <param name="entity">Обёрнутая сущность, свойства которой устанавливаются</param>
    /// <param name="wrapper">Обёртка сущности</param>
    protected void _setEntityPropertiesFromWrapper(DiskDrive entity, DiskDriveWrapper wrapper) {
        base._setEntityPropertiesFromWrapper(entity, wrapper);
        entity.Capacity = wrapper.Capacity;
        entity.ReadSpeed = wrapper.ReadSpeed;
        entity.WriteSpeed = wrapper.WriteSpeed;
    }
    
    /// <summary>
    /// Установка свойств обёртки, общих с сущностью
    /// </summary>
    /// <param name="entity">Оборачиваемая сущность</param>
    /// <param name="wrapper">Обёртка, свойства которой устанавливаются</param>
    protected void _setWrapperPropertiesFromEntity(DiskDrive entity, DiskDriveWrapper wrapper) {
        base._setWrapperPropertiesFromEntity(entity, wrapper);
        wrapper.Capacity = entity.Capacity;
        wrapper.ReadSpeed = entity.ReadSpeed;
        wrapper.WriteSpeed = entity.WriteSpeed;
    }
}