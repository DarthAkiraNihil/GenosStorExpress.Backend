namespace GenosStorExpress.Domain.Entity.Item;

public class Review {
    public long Id { get; set; }
    
    public byte Rating { get; set; }
    public virtual Item Item { get; set; }
}