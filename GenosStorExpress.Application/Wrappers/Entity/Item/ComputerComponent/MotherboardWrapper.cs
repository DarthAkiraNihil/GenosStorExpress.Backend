using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class MotherboardWrapper: ComputerComponentWrapper {
    public IList<CPUCoreWrapper> SupportedCPUCores { get; set; }
    public IList<string> SupportedRAMTypes { get; set; }
    
    public byte RAMSlots { get; set; }
    public byte RAMChannels { get; set; }
    public int MaxRAMFrequency { get; set; }
    public byte PCIESlotsCount { get; set; }
    
    public bool HasNVMeSupport { get; set; }
    public byte M2SlotsCount { get; set; }
    public byte SataPortsCount { get; set; }
    public byte USBPortsCount { get; set; }
    public List<string> VideoPorts { get; set; }
    public byte RJ45PortsCount { get; set; }
    public byte DigitalAudioPortsCount { get; set; }

    public float NetworkAdapterSpeed { get; set; }
		
    public string FormFactor { get; set; }
    public string CPUSocket { get; set; }
    public string PCIEVersion { get; set; }
		
    public MotherboardChipsetWrapper MotherboardChipset { get; set; }
    public AudioChipsetWrapper AudioChipset { get; set; }
    public NetworkAdapterWrapper NetworkAdapter { get; set; }

    public MotherboardWrapper() {
	    SupportedCPUCores = new List<CPUCoreWrapper>();
	    SupportedRAMTypes = new List<string>();
	    VideoPorts = new List<string>();
	    
	    FormFactor = string.Empty;
	    CPUSocket = string.Empty;
	    PCIEVersion = string.Empty;
	    
	    MotherboardChipset = new MotherboardChipsetWrapper();
	    AudioChipset = new AudioChipsetWrapper();
	    NetworkAdapter = new NetworkAdapterWrapper();
	    
    }
}