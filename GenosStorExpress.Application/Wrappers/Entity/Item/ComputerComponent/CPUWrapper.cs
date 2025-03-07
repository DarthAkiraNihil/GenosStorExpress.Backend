namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class CPUWrapper: ComputerComponentWrapper {
    public string CPUCore { get; set; }
    public string CPUSocket { get; set; }
    
    public int CoresCount { get; set; }
    public int ThreadsCount { get; set; }
    public float L2CahceSize { get; set; }
    public float L3CacheSize { get; set; }
    public float TechnicalProcess {  get; set; }
    public float BaseFrequency { get; set; }
    public List<string> SupportedRamType { get; set; }
    public int SupportedRAMSize { get; set; }
    public bool HasIntegratedGraphics { get; set; }
}