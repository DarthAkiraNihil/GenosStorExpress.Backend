namespace GenosStorExpress.Application.Wrappers.Entity.Base;

public abstract class NamedWrapper {
    public string Name { get; set; }

    protected NamedWrapper() {
        Name = string.Empty;
    }
}