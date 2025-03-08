using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Application.Service.Implementation;

public abstract class StandardService {
    protected void _setItemFromEntityToWrapper(ItemWrapper wrapper, Item item) {
        wrapper.ItemType = item.ItemType.Name;
        wrapper.Name = item.Name;
        wrapper.Description = item.Description;
        wrapper.Price = item.Price;
    }
}