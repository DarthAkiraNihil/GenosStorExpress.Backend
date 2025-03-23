namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

public class ShortOrderWrapper {
    public long OrderId { get; set; }
    public string Status { get; set; }

    public ShortOrderWrapper() {
        Status = string.Empty;
    }
}