using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class CPUWrapper: ComputerComponentWrapper {
    public CPUCoreWrapper Core { get; set; }
    public string Socket { get; set; }
    
    public int CoresCount { get; set; }
    public int ThreadsCount { get; set; }
    public float L2CacheSize { get; set; }
    public float L3CacheSize { get; set; }
    public float TechnicalProcess {  get; set; }
    public float BaseFrequency { get; set; }
    public List<string> SupportedRamTypes { get; set; }
    public int SupportedRAMSize { get; set; }
    public bool HasIntegratedGraphics { get; set; }

    public CPUWrapper() {
        SupportedRamTypes = new List<string>();
        Socket = string.Empty;
        Core = new CPUCoreWrapper();
    }
}