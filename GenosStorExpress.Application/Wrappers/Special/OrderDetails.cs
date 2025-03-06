namespace GenosStorExpress.Application.Wrappers.Special {
    public class OrderDetails {
        public long Id { get; set; }
        public string OrderTitle { get; set; }
        public string OrderCreatedAt { get; set; }
        public string OrderStatus { get; set; }
        public double Total { get; set; }
    }
}