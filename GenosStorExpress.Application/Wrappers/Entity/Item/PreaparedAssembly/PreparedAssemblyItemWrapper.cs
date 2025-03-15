namespace GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;

public class PreparedAssemblyItemWrapper {
    public int Id { get; set; }
    public string Model { get; set; }

    public PreparedAssemblyItemWrapper() {
        Model = string.Empty;
    }
}