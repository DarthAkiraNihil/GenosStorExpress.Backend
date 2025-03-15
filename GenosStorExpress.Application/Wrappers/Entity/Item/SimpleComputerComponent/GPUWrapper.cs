using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

public class GPUWrapper: WithModelWrapper {
    public int Id { get; set; }
    public string Vendor { get; set; }

    public GPUWrapper() {
        Vendor = string.Empty;
    }
}