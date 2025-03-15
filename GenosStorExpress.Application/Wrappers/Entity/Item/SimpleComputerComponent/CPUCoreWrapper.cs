using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

public class CPUCoreWrapper: NamedWrapper {
    public int Id { get; set; }
    public string Vendor { get; set; }

    public CPUCoreWrapper() {
        Vendor = string.Empty;
    }
}