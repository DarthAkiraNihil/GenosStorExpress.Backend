namespace GenosStorExpress.Domain.Interface.Item.ComputerComponent {
    public interface IComputerComponentRepository {
        // ComputerComponent
        IComputerCaseRepository ComputerCases { get; }
        //IComputerComponentRepository ComputerComponents { get; }
        ICPUCoolerRepository CPUCoolers { get; }
        ICPURepository CPUs { get; }
        //IDiskDriveRepository DiskDrives { get; }
        IDisplayRepository Displays { get; }
        IGraphicsCardRepository GraphicsCards { get; }
        IHDDRepository HDDs { get; }
        IKeyboardRepository Keyboards { get; }
        IMotherboardRepository Motherboards { get; }
        IMouseRepository Mouses { get; }
        INVMeSSDRepository NVMeSSDs { get; }
        IPowerSupplyRepository PowerSupplies { get; }
        IRAMRepository RAMs { get; }
        ISataSSDRepository SataSSDs { get; }
        //ISSDRepository SSDs { get; }
    }
}