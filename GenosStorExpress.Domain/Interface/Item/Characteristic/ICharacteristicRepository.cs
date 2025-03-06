namespace GenosStorExpress.Domain.Interface.Item.Characteristic {
    public interface ICharacteristicRepository {
        
        ICertificate80PlusRepository Certificates80Plus { get; }
        IComputerCaseTypesizeRepository ComputerCaseTypesizes { get; }
        ICoolerMaterialRepository CoolerMaterials { get; }
        ICPUSocketRepository CPUSockets { get; }
        IDefinitionRepository Definitions { get; }
        IDPIModeRepository DPIModes { get; }
        IKeyboardTypeRepository KeyboardTypes { get; }
        IKeyboardTypesizeRepository KeyboardTypesizes { get; }
        IMatrixTypeRepository MatrixTypes { get; }
        IMotherboardFormFactorRepository MotherboardFormFactors { get; }
        IPCIEVersionRepository PCIEVersions { get; }
        IRAMTypeRepository RAMTypes { get; }
        IUnderlightRepository Underlights { get; }
        IVendorRepository Vendors { get; }
        IVesaSizeRepository VesaSizes { get; }
        IVideoPortRepository VideoPorts { get; }
        
    }
}