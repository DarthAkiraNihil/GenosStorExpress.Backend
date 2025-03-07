using GenosStorExpress.Application.Wrappers.Entity.Base;
using GenosStorExpress.Domain.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

public class GPUWrapper: WithModelWrapper {
    public int Id { get; set; }
    public string Vendor { get; set; }
}