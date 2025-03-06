using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Interface.Common {
    public interface IPaymentService {
        string GetOrdererInfo(Customer customer);
        bool ProcessPayment(Order order);
    }
}