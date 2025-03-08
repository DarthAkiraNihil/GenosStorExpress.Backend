using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

public abstract class AbstractDiskDriveService: AbstractComputerComponentService {
    protected AbstractDiskDriveService(IItemTypeService itemTypeService, IVendorService vendorService) : base(itemTypeService, vendorService) {
    }
    
    protected void _setEntityPropertiesFromWrapper(DiskDrive entity, DiskDriveWrapper wrapper) {
        base._setEntityPropertiesFromWrapper(entity, wrapper);
        entity.Capacity = wrapper.Capacity;
        entity.ReadSpeed = wrapper.ReadSpeed;
        entity.WriteSpeed = wrapper.WriteSpeed;
    }
    
    protected void _setWrapperPropertiesFromEntity(DiskDrive entity, DiskDriveWrapper wrapper) {
        base._setWrapperPropertiesFromEntity(entity, wrapper);
        wrapper.Capacity = entity.Capacity;
        wrapper.ReadSpeed = entity.ReadSpeed;
        wrapper.WriteSpeed = entity.WriteSpeed;
    }
}