using System.ComponentModel.DataAnnotations;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Domain.Entity.Item;

public class Review {
    public long Id { get; set; }
    
    public byte Rating { get; set; }
    [MaxLength(Int32.MaxValue)]
    public string Comment { get; set; }
    public Item Item { get; set; }
    
    public string? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public Review() {
        Comment = string.Empty;
        Item = new VoidItem();
    }
}