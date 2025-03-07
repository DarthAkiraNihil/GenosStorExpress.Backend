using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public abstract class SSDWrapper: DiskDriveWrapper {
    public int TBW { get; set; }
    public float DWPD { get; set; }
    public byte BitsForCell { get; set; }
    public SSDControllerWrapper SSDController { get; set; }
}