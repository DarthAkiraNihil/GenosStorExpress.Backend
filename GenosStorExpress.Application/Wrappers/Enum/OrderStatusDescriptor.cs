namespace GenosStorExpress.Application.Wrappers.Enum {
    public enum OrderStatusDescriptor {
        Created = 1,
        Confirmed,
        AwaitsPayment,
        Paid,
        Processing,
        Delivering,
        Received,
        Cancelled,
    }
}