namespace GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;

public class PreparedAssemblyWrapper: ItemWrapper {
    
    public IList<PreparedAssemblyItemWrapper> RAMs { get; set; }
    public IList<PreparedAssemblyDiskDriveWrapper> DiskDrives { get; set; }
    public PreparedAssemblyItemWrapper CPU { get; set; }
    public PreparedAssemblyItemWrapper Motherboard { get; set; }
    public PreparedAssemblyItemWrapper GraphicsCard { get; set; }
    public PreparedAssemblyItemWrapper PowerSupply { get; set; }
    public PreparedAssemblyItemWrapper? Display { get; set; }
    public PreparedAssemblyItemWrapper ComputerCase { get; set; }
    public PreparedAssemblyItemWrapper? Keyboard { get; set; }
    public PreparedAssemblyItemWrapper? Mouse { get; set; }
    public PreparedAssemblyItemWrapper CPUCooler { get; set; }

    public PreparedAssemblyWrapper() {
        RAMs = new List<PreparedAssemblyItemWrapper>();
        DiskDrives = new List<PreparedAssemblyDiskDriveWrapper>();
        CPU = new PreparedAssemblyItemWrapper();
        Motherboard = new PreparedAssemblyItemWrapper();
        GraphicsCard = new PreparedAssemblyItemWrapper();
        PowerSupply = new PreparedAssemblyItemWrapper();
        ComputerCase = new PreparedAssemblyItemWrapper();
        CPUCooler = new PreparedAssemblyItemWrapper();
    }
}