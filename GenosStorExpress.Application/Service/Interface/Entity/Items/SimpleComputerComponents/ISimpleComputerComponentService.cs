namespace GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents {
    public interface ISimpleComputerComponentService {
        ISSDControllerService SSDControllers { get; }
        ISimpleComputerComponentTypeService Types { get; }
        INetworkAdapterService NetworkAdapters { get; }
        IMotherboardChipsetService MotherboardChipsets { get; }
        IGPUService GPUs { get; }
        ICPUCoreService CPUCores { get; }
        IAudioChipsetService AudioChipsets { get; }
    }
}