namespace GenosStorExpress.Domain.Entity.Item;

public class Review {
    public long Id { get; set; }
    
    public byte Rating { get; set; }
    public string Comment { get; set; }
    public Item Item { get; set; }

    public Review() {
        Comment = string.Empty;
    }
}