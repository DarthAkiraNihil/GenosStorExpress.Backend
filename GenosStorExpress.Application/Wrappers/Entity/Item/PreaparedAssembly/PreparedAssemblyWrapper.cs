namespace GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;

public class PreparedAssemblyWrapper: ItemWrapper {
    
    public List<PreparedAssemblyItemWrapper> RAMs { get; set; }
    public List<PreparedAssemblyDiskDriveWrapper> DiskDrives { get; set; }
    public PreparedAssemblyItemWrapper CPU { get; set; }
    public PreparedAssemblyItemWrapper Motherboard { get; set; }
    public PreparedAssemblyItemWrapper GraphicsCard { get; set; }
    public PreparedAssemblyItemWrapper PowerSupply { get; set; }
    public PreparedAssemblyItemWrapper? Display { get; set; }
    public PreparedAssemblyItemWrapper ComputerCase { get; set; }
    public PreparedAssemblyItemWrapper? Keyboard { get; set; }
    public PreparedAssemblyItemWrapper? Mouse { get; set; }
    public PreparedAssemblyItemWrapper CPUCooler { get; set; }
    
}