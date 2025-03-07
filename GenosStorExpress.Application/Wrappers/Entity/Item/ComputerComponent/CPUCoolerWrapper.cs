namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class CPUCoolerWrapper: ComputerComponentWrapper {
    public long MaxFanRPM { get; set; }
    public byte TubesCount { get; set; }
    public float TubesDiameter { get; set; }
    public byte FanCount { get; set; }
		
    public string FoundationMaterial { get; set; }
    public string RadiatorMaterial { get; set; }
}