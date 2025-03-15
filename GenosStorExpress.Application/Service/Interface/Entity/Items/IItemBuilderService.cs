using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items;

public interface IItemBuilderService {
    
    ComputerCaseWrapper BuildComputerCase(AnonymousItemWrapper wrapper);
    CPUCoolerWrapper BuildCPUCooler(AnonymousItemWrapper wrapper);
    CPUWrapper BuildCPU(AnonymousItemWrapper wrapper);
    DisplayWrapper BuildDisplay(AnonymousItemWrapper wrapper);
    GraphicsCardWrapper BuildGraphicsCard(AnonymousItemWrapper wrapper);
    HDDWrapper BuildHDD(AnonymousItemWrapper wrapper);
    KeyboardWrapper BuildKeyboard(AnonymousItemWrapper wrapper);
    MotherboardWrapper BuildMotherboard(AnonymousItemWrapper wrapper);
    MouseWrapper BuildMouse(AnonymousItemWrapper wrapper);
    NVMeSSDWrapper BuildNVMeSSD(AnonymousItemWrapper wrapper);
    PowerSupplyWrapper BuildPowerSupply(AnonymousItemWrapper wrapper);
    RAMWrapper BuildRAM(AnonymousItemWrapper wrapper);
    SataSSDWrapper BuildSataSSD(AnonymousItemWrapper wrapper);
    
    AnonymousItemWrapper BuildWrapper(ComputerCaseWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(CPUCoolerWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(CPUWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(DisplayWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(GraphicsCardWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(HDDWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(KeyboardWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(MotherboardWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(MouseWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(NVMeSSDWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(PowerSupplyWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(RAMWrapper wrapper);
    AnonymousItemWrapper BuildWrapper(SataSSDWrapper wrapper);
    
}