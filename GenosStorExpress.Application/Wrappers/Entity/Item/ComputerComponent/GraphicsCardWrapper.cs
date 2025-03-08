using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class GraphicsCardWrapper: ComputerComponentWrapper {
    public int VideoRAM { get; set; }
    public List<string> VideoPorts { get; set; }
    public byte MaxDisplaysSupported { get; set; }
    public byte UsedSlots { get; set; }

    public GPUWrapper GPU { get; set; }
}