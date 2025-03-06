namespace GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics {
    public interface ICharacteristicsService {
        ICertificate80PlusService Certificates80Plus { get; }
        IComputerCaseTypesizeService ComputerCaseTypesizes { get; }
        ICoolerMaterialService CoolerMaterials { get; }
        ICPUSocketService CPUSockets { get; }
        IDefinitionService Definitions { get; }
        IDPIModeService DPIModes { get; }
        IKeyboardTypeService KeyboardTypes { get; }
        IKeyboardTypesizeService KeyboardTypesizes { get; }
        IMatrixTypeService MatrixTypes { get; }
        IMotherboardFormFactorService MotherboardFormFactors { get; }
        // IPCIEVersionService PCIEVersions { get; }
        IRAMTypeService RAMTypes { get; }
        IUnderlightService Underlights { get; }
        IVendorService Vendors { get; }
        IVesaSizeService VesaSizes { get; }
        IVideoPortService VideoPorts { get; }
    }
}