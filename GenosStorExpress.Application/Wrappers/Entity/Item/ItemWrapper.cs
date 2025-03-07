using GenosStorExpress.Application.Wrappers.Entity.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.Orders;

namespace GenosStorExpress.Application.Wrappers.Entity.Item;

public abstract class ItemWrapper: WithModelWrapper {
    public int Id { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public ActiveDiscountWrapper ActiveDiscount { get; set; }
    public string ItemType { get; set; }
}