using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

public abstract class SimpleComputerComponentWrapper: WithModelWrapper {
    public long Id { get; set; }
    public string Type { get; set; }
}