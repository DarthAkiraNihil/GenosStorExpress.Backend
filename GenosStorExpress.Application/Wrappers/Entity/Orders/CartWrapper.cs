namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

public class CartWrapper {
    public IList<CartItemWrapper> Items { get; set; }

    public CartWrapper() {
        Items = new List<CartItemWrapper>();
    }
}