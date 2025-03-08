using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Application.Service.Implementation.Base;

public abstract class AbstractItemService {
    protected readonly IItemTypeService _itemTypeService;

    protected AbstractItemService(IItemTypeService itemTypeService) {
        _itemTypeService = itemTypeService;
    }

    protected void _setEntityPropertiesFromWrapper(Item entity, ItemWrapper wrapper) {
        entity.Name = wrapper.Name;
        entity.Model = wrapper.Model;
        entity.Price = wrapper.Price;
        entity.ImageBase64 = "";
        entity.Description = wrapper.Description;
        entity.ItemType = _itemTypeService.GetEntityFromString(wrapper.ItemType);
    }

    protected void _setWrapperPropertiesFromEntity(Item entity, ItemWrapper wrapper) {
        wrapper.Id = entity.Id;
        wrapper.Name = entity.Name;
        wrapper.Model = entity.Model;
        wrapper.Price = entity.Price;
        wrapper.Description = entity.Description;
        wrapper.ItemType = entity.ItemType.Name;
    }
}