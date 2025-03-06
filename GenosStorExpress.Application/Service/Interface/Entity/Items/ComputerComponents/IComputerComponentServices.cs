namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    public interface IComputerComponentServices {
        IComputerCaseService ComputerCases { get; }
        ICPUCoolerService CPUCoolers { get; }
        ICPUService CPUs { get; }
        //IDiskDriveService DiskDrives { get; }
        IDisplayService Displays { get; }
        IGraphicsCardService GraphicsCards { get; }
        IHDDService HDDs { get; }
        IKeyboardService Keyboards { get; }
        IMotherboardService Motherboards { get; }
        IMouseService Mouses { get; }
        INVMeSSDService NVMeSSDs { get; }
        IPowerSupplyService PowerSupplies { get; }
        IRAMService RAMs { get; }
        ISataSSDService SataSSDs { get; }
        // ISSDService SSDs { get; }
    }
}