using GenosStorExpress.Application.Wrappers.Entity.Orders;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface IOrderService {
        OrderWrapper? Get(int orderId, string customerId);
        IList<OrderWrapper> List(string customerId);
        ShortOrderWrapper CreateOrderFromCart(string customerId);
        double CalculateTotal(int orderId);
        List<ShortOrderWrapper> ListOfSpecificCustomer(string customerId);
        void ReceiveOrder(int orderId);
        void CancelOrder(int orderId);
        List<ShortOrderWrapper> GetActiveOrders();
        bool OrderExists(int orderId);
    }
}