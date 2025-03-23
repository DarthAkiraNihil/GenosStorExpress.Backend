using System.Text.Json.Serialization;
using GenosStorExpress.Application.Wrappers.Entity.Item;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

public class CartItemWrapper {
    [JsonPropertyName("item_id")]
    public ItemWrapper Item { get; set; }
    public int Quantity { get; set; }

    public CartItemWrapper() {
        Item = new ItemWrapper();
    }
}