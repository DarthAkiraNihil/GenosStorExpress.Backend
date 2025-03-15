namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class ComputerComponentWrapper: ItemWrapper {
    public double TDP { get; set; }
    public string Vendor { get; set; }

    public ComputerComponentWrapper() {
        Vendor = string.Empty;
    }
}