namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class PowerSupplyWrapper: ComputerComponentWrapper {
    public byte SataPorts { get; set; }
    public byte MolexPorts { get; set; }
    public int PowerOutput { get; set; }
		
    public string Certificate80Plus { get; set; }

    public PowerSupplyWrapper() {
        Certificate80Plus = string.Empty;
    }
}