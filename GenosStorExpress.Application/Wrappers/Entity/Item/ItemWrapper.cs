using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item;

public abstract class ItemWrapper: WithModelWrapper {
    public int Id { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string ItemType { get; set; }

    protected ItemWrapper() {
        Description = string.Empty;
        ItemType = string.Empty;
    }
}