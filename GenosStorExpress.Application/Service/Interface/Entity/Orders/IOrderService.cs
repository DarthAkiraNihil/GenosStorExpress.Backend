using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface IOrderService: ISupportsSave {
        OrderWrapper? Get(int orderId, string customerId);
        IList<OrderWrapper> List(string customerId);
        ShortOrderWrapper CreateOrderFromCart(string customerId);
        double CalculateTotal(int orderId);
        void ReceiveOrder(int orderId);
        void CancelOrder(int orderId);
        List<ShortOrderWrapper> GetActiveOrders();
        List<Order> GetActiveOrdersRaw(string sudoId);
        bool OrderExists(int orderId);
    }
}