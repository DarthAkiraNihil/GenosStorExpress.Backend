using GenosStorExpress.Application.Wrappers.Entity.Item.Orders;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface IActiveDiscountService: 
        ISupportsCreate<ActiveDiscountWrapper>,
        ISupportsGet<ActiveDiscountWrapper>,
        ISupportsList<ActiveDiscountWrapper>,
        ISupportsUpdateWrapped<ActiveDiscountWrapper>,
        ISupportsDelete,
        ISupportsSave {
		bool IsActive(ActiveDiscountWrapper activeDiscount);
        void Deactivate(int id);
    }
}