using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items;

public interface IItemServiceRouter {
    AnonymousItemWrapper Get(ItemTypeDescriptor itemType, int id);
    IList<AnonymousItemWrapper> List(ItemTypeDescriptor itemType);
    void Create(ItemTypeDescriptor itemType, AnonymousItemWrapper item);
    void Update(ItemTypeDescriptor itemType, int id, AnonymousItemWrapper item);
    void Delete(ItemTypeDescriptor itemType, int id);
    void Save(ItemTypeDescriptor itemType);
}