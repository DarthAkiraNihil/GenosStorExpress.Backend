using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

public abstract class AbstractSimpleComputerComponentService {
    private readonly ISimpleComputerComponentTypeService _simpleComputerComponentTypeService;

    protected AbstractSimpleComputerComponentService(ISimpleComputerComponentTypeService simpleComputerComponentTypeService) {
        _simpleComputerComponentTypeService = simpleComputerComponentTypeService;
    }
    
    protected void _setEntityPropertiesFromWrapper(SimpleComputerComponent entity, SimpleComputerComponentWrapper wrapper) {
        entity.Name = wrapper.Name;
        entity.Model = wrapper.Model;
        entity.Type = _simpleComputerComponentTypeService.GetEntityFromString(wrapper.Type);
    }

    protected void _setWrapperPropertiesFromEntity(SimpleComputerComponent entity, SimpleComputerComponentWrapper wrapper) {
        wrapper.Id = entity.Id;
        wrapper.Name = entity.Name;
        wrapper.Model = entity.Model;
        wrapper.Type = entity.Type.Name;
    }
}