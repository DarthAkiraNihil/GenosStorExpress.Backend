using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

public abstract class AbstractSSDService: AbstractDiskDriveService {
    protected readonly ISSDControllerService _ssdControllerService;

    protected AbstractSSDService(IItemTypeService itemTypeService, IVendorService vendorService, ISSDControllerService ssdControllerService) : base(itemTypeService, vendorService) {
        _ssdControllerService = ssdControllerService;
    }
    
    protected void _setEntityPropertiesFromWrapper(SSD entity, SSDWrapper wrapper) {
        base._setEntityPropertiesFromWrapper(entity, wrapper);
        entity.TBW = wrapper.TBW;
        entity.DWPD = wrapper.DWPD;
        entity.BitsForCell = wrapper.BitsForCell;
        entity.Controller = _ssdControllerService.GetRaw((int) wrapper.Controller.Id);
    }

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