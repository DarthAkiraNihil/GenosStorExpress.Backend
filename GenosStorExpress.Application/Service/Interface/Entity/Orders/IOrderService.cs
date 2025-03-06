using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface IOrderService: IStandardService<Order> {
        long CreateOrderFromCart(Customer customer);
        double CalculateTotal(Order order);
        List<Order> ListOfSpecificCustomer(Customer customer);
        void ReceiveOrder(Order order);
        void CancelOrder(Order order);
        List<Order> GetActiveOrders();
        bool OrderExists(int orderId);
    }
}