namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface IOrderEntitiesService {
        IActiveDiscountService ActiveDiscounts { get; }
        IBankCardService BankCards { get; }
        IBankSystemService BankSystems { get; }
        ICartService Carts { get; }
        // IOrderItemsService OrderItems { get; }
        IOrderService Orders { get; }
        IOrderStatusService OrderStatuses { get; }
    }
}