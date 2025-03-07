
namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class DiskDriveWrapper: ComputerComponentWrapper {
    public long Capacity { get; set; }
    public long ReadSpeed { get; set; }
    public long WriteSpeed { get; set; }
}