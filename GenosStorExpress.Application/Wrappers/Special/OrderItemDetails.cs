using GenosStorExpress.Domain.Entity.Orders;

namespace GenosStorExpress.Application.Wrappers.Special {
    public class OrderItemDetails {
        public OrderItems Item { get; set; }
        public double Subtotal { get; set; }
    }
}