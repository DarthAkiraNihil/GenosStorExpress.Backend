namespace GenosStorExpress.Domain.Interface.Orders {
    public interface IOrderEntitiesRepository {
        // Orders
        IActiveDiscountRepository ActiveDiscounts { get; }
        IBankCardRepository BankCards { get; }
        IBankSystemRepository BankSystems { get; }
        ICartRepository Carts { get; }
        ICartItemsRepository CartItems { get; }
        IOrderItemsRepository OrderItems { get; }
        IOrderRepository Orders { get; }
        IOrderStatusRepository OrderStatuses { get; }
    }
}