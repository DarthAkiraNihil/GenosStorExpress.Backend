using GenosStorExpress.Domain.Entity.Orders;

namespace GenosStorExpress.Application.Wrappers.Special {
    public class CartListElement {
        public CartItem Item { get; set; }
        public double? Price { get; set; }
        public double? DiscountedPrice { get; set; }
        public double? OldPrice { get; set; }
    }
}