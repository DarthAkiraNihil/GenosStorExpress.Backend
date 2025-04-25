namespace GenosStorExpress.Application.Service.Interface.Common {
    public interface IPaymentService {
        string GetOrdererInfo(string customerId);
        bool ProcessPayment(int orderId, int bankCardId, string customerId);
    }
}