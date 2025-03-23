namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

public class OrderWrapper {
    public long Id { get; set; }
    public IList<OrderItemWrapper> Items { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public OrderWrapper() {
        Items = new List<OrderItemWrapper>();
        Status = string.Empty;
        CreatedAt = DateTime.Now;
    }
}