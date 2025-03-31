using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

/// <summary>
/// Абстрактный класс, предоставляющий методы, общие для всех сервисов твердотельных накопителей
/// </summary>
public abstract class AbstractSSDService: AbstractDiskDriveService {
    private readonly ISSDControllerService _ssdControllerService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="itemTypeService">Сервис типов товаров</param>
    /// <param name="vendorService">Сервис производителей</param>
    /// <param name="ssdControllerService">Сервис контроллеров твердотельных накопителей</param>
    protected AbstractSSDService(IItemTypeService itemTypeService, IVendorService vendorService, ISSDControllerService ssdControllerService) : base(itemTypeService, vendorService) {
        _ssdControllerService = ssdControllerService;
    }
    
    /// <summary>
    /// Установка свойств сущности, общих с обёрткой
    /// </summary>
    /// <param name="entity">Обёрнутая сущность, свойства которой устанавливаются</param>
    /// <param name="wrapper">Обёртка сущности</param>
    /// <exception cref="NullReferenceException">Если указан несуществующий контроллер</exception>
    protected void _setEntityPropertiesFromWrapper(SSD entity, SSDWrapper wrapper) {
        base._setEntityPropertiesFromWrapper(entity, wrapper);
        entity.TBW = wrapper.TBW;
        entity.DWPD = wrapper.DWPD;
        entity.BitsForCell = wrapper.BitsForCell;

        var controller = _ssdControllerService.GetRaw((int)wrapper.Controller.Id);
        if (controller == null) {
            throw new NullReferenceException($"Контроллера твердотельного накопителя с номером {wrapper.Controller.Id} существует");
        }

        entity.Controller = controller;
    }

    /// <summary>
    /// Установка свойств обёртки, общих с сущностью
    /// </summary>
    /// <param name="entity">Оборачиваемая сущность</param>
    /// <param name="wrapper">Обёртка, свойства которой устанавливаются</param>
    protected void _setWrapperPropertiesFromEntity(SSD entity, SSDWrapper wrapper) {
        base._setWrapperPropertiesFromEntity(entity, wrapper);
        wrapper.TBW = entity.TBW;
        wrapper.DWPD = entity.DWPD;
        wrapper.BitsForCell = entity.BitsForCell;
        wrapper.Controller = new SSDControllerWrapper {
            Id = entity.Controller.Id,
            Model = entity.Controller.Model,
            Name = entity.Controller.Name,
            Type = entity.Controller.Type.Name
        };
    }
}