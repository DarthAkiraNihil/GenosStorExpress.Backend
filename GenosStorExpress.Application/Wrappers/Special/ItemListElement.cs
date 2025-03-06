using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Application.Wrappers.Special {
    public class ItemListElement<T> where T : Item {
        public T Item { get; set; }
        public double? Price { get; set; }
        public double? DiscountedPrice { get; set; }
        public double? OldPrice { get; set; }
        public string DiscountLabel { get; set; }
    }
}