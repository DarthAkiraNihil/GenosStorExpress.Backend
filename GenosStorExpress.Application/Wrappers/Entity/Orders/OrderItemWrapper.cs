using GenosStorExpress.Application.Wrappers.Entity.Item;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

public class OrderItemWrapper {
    public ItemWrapper Item { get; set; }
    public int Quantity { get; set; }
    public double BoughtFor { get; set; }

    public OrderItemWrapper() {
        Item = new ItemWrapper();
    }
}