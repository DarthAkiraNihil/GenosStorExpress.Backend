using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Utils.Operations;


namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    /// <summary>
    /// Интерфейс сервиса скидок
    /// </summary>
    public interface IActiveDiscountService: 
        // ISupportsCreate<DetailedActiveDiscountWrapper>,
        // ISupportsGet<DetailedActiveDiscountWrapper>,
        // ISupportsList<ActiveDiscountWrapper>,
        //ISupportsUpdateWrapped<DetailedActiveDiscountWrapper>,
        ISupportsSave {
        void Activate(int itemId, ActiveDiscountWrapper discountData);
        void Edit(int discountId, ActiveDiscountWrapper discountData);
        /// <summary>
        /// Деактивация скидки. Все товары с указанной скидкой теряют её
        /// </summary>
        /// <param name="id">Номер скидки</param>
        void Deactivate(int id);
    }
}