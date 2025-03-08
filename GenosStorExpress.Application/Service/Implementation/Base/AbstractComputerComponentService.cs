using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

public abstract class AbstractComputerComponentService: AbstractItemService {
    protected readonly IVendorService _vendorService;

    protected AbstractComputerComponentService(IItemTypeService itemTypeService, IVendorService vendorService) : base(itemTypeService) {
        _vendorService = vendorService;
    }

    protected void _setEntityPropertiesFromWrapper(ComputerComponent entity, ComputerComponentWrapper wrapper) {
        base._setEntityPropertiesFromWrapper(entity, wrapper);
        entity.TDP = wrapper.TDP;
        entity.Vendor = _vendorService.GetEntityFromString(wrapper.Vendor);
    }
    
    protected void _setWrapperPropertiesFromEntity(ComputerComponent entity, ComputerComponentWrapper wrapper) {
        base._setWrapperPropertiesFromEntity(entity, wrapper);
        wrapper.TDP = entity.TDP;
        wrapper.Vendor = entity.Vendor.Name;
    }
}