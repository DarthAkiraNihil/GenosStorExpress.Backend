namespace GenosStorExpress.Application.Wrappers.Entity.Item.Orders;

public class BankCardWrapper {
    public int Id { get; set; }
    public long Number { get; set; }
    public byte ValidThruMonth { get; set; }
    public byte ValidThruYear { get; set; }
    public byte CVC { get; set; }
    public string Owner { get; set; }
    public string BankSystem { get; set; }
}