using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item;

public class ItemWrapper: WithModelWrapper {
    /// <summary>
    /// Номер товара
    /// </summary>
    public int Id { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string ItemType { get; set; }

    public ItemWrapper() {
        Description = string.Empty;
        ItemType = string.Empty;
    }
}