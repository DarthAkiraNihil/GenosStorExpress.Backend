namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class KeyboardWrapper: ComputerComponentWrapper {
    public bool HasRGBLighting { get; set; }
    public bool IsWireless { get; set; }
		
    public string Typesize { get; set; }
    public string Type { get; set; }
}