using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items {
    public interface IItemTypeService: IEnumService<ItemType> {
        ItemTypeDescriptor GetDescriptor(string name);
    }
}