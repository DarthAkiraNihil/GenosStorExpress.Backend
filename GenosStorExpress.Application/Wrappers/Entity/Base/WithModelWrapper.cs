namespace GenosStorExpress.Application.Wrappers.Entity.Base;

public abstract class WithModelWrapper: NamedWrapper {
    public string Model { get; set; }

    protected WithModelWrapper() {
        Model = string.Empty;
    }
}