namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class MouseWrapper: ComputerComponentWrapper {
    public byte ButtonsCount { get; set; }
    public bool HasProgrammableButtons { get; set; }
    public List<int> DPIModes { get; set; }
    public bool IsWireless { get; set; }
}