using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items {
    public interface IAllItemsService :
        ISupportsGet<ItemWrapper>,
        ISupportsList<ItemWrapper>,
        ISupportsDelete,
        ISupportsSave;
}