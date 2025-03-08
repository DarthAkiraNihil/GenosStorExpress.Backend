namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class RAMWrapper: ComputerComponentWrapper {
    public int TotalSize { get; set; }
    public int ModuleSize { get; set; }
    public byte ModulesCount { get; set; }
    public int Frequency { get; set; }
    public byte CL { get; set; }
    public byte tRCD { get; set; }
    public byte tRP { get; set; }
    public byte tRAS { get; set; }
		
    public string Type { get; set; }
}