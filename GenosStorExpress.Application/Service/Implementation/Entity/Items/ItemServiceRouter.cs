using System.Collections;
using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Interface.Item;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items;

public class ItemServiceRouter: IItemServiceRouter {
    
    private readonly IComputerComponentServices _computerComponents;
    private readonly IPreparedAssemblyService _preparedAssemblyService;
    private readonly IItemBuilderService _itemBuilderService;

    public ItemServiceRouter(IComputerComponentServices computerComponents, IPreparedAssemblyService preparedAssemblyService, IItemBuilderService itemBuilderService) {
        _computerComponents = computerComponents;
        _preparedAssemblyService = preparedAssemblyService;
        _itemBuilderService = itemBuilderService;
    }

    public AnonymousItemWrapper Get(ItemTypeDescriptor itemType, int id) {
        
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.ComputerCases.Get(id)
                );
            }
            case ItemTypeDescriptor.CPUCooler: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.CPUCoolers.Get(id)
                );
            }
            case ItemTypeDescriptor.CPU: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.CPUs.Get(id)
                );
            }
            case ItemTypeDescriptor.Display: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.Displays.Get(id)
                );
            }
            case ItemTypeDescriptor.GraphicsCard: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.GraphicsCards.Get(id)
                );
            }
            case ItemTypeDescriptor.HDD: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.HDDs.Get(id)
                );
            }
            case ItemTypeDescriptor.Keyboard: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.Keyboards.Get(id)
                );
            }
            case ItemTypeDescriptor.Motherboard: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.Motherboards.Get(id)
                );
            }
            case ItemTypeDescriptor.Mouse: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.Mouses.Get(id)
                );
            }
            case ItemTypeDescriptor.NVMeSSD: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.NVMeSSDs.Get(id)
                );
            }
            case ItemTypeDescriptor.PowerSupply: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.PowerSupplies.Get(id)
                );
            }
            case ItemTypeDescriptor.RAM: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.RAMs.Get(id)
                );
            }
            case ItemTypeDescriptor.SataSSD: {
                return _itemBuilderService.BuildWrapper(
                    _computerComponents.SataSSDs.Get(id)
                );
            }
            // case ItemTypeDescriptor.PreparedAssembly: {
            //     return _itemBuilderService.BuildWrapper(
            //         _preparedAssemblyService.Get(id)
            //     );
            // }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    public IList<AnonymousItemWrapper> List(ItemTypeDescriptor itemType) {
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                return _computerComponents.ComputerCases.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.CPUCooler: {
                return _computerComponents.CPUCoolers.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.CPU: {
                return _computerComponents.CPUs.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.Display: {
                return _computerComponents.Displays.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.GraphicsCard: {
                return _computerComponents.GraphicsCards.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.HDD: {
                return _computerComponents.HDDs.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.Keyboard: {
                return _computerComponents.Keyboards.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.Motherboard: {
                return _computerComponents.Motherboards.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.Mouse: {
                return _computerComponents.Mouses.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.NVMeSSD: {
                return _computerComponents.NVMeSSDs.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.PowerSupply: {
                return _computerComponents.PowerSupplies.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.RAM: {
                return _computerComponents.RAMs.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            case ItemTypeDescriptor.SataSSD: {
                return _computerComponents.SataSSDs.List().Select(
                    i => _itemBuilderService.BuildWrapper(i)
                ).ToList();
            }
            // case ItemTypeDescriptor.PreparedAssembly: {
            //     return _itemBuilderService.BuildWrapper(
            //         _preparedAssemblyService.Get(id)
            //     );
            // }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    public void Create(ItemTypeDescriptor itemType, AnonymousItemWrapper item) {
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                _computerComponents.ComputerCases.Create(_itemBuilderService.BuildComputerCase(item));
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                _computerComponents.CPUCoolers.Create(_itemBuilderService.BuildCPUCooler(item));
                return;
            }
            case ItemTypeDescriptor.CPU: {
                _computerComponents.CPUs.Create(_itemBuilderService.BuildCPU(item));
                return;
            }
            case ItemTypeDescriptor.Display: {
                _computerComponents.Displays.Create(_itemBuilderService.BuildDisplay(item));
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                _computerComponents.GraphicsCards.Create(_itemBuilderService.BuildGraphicsCard(item));
                return;
            }
            case ItemTypeDescriptor.HDD: {
                _computerComponents.HDDs.Create(_itemBuilderService.BuildHDD(item));
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                _computerComponents.Keyboards.Create(_itemBuilderService.BuildKeyboard(item));
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                _computerComponents.Motherboards.Create(_itemBuilderService.BuildMotherboard(item));
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                _computerComponents.Mouses.Create(_itemBuilderService.BuildMouse(item));
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                _computerComponents.NVMeSSDs.Create(_itemBuilderService.BuildNVMeSSD(item));
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                _computerComponents.PowerSupplies.Create(_itemBuilderService.BuildPowerSupply(item));
                return;
            }
            case ItemTypeDescriptor.RAM: {
                _computerComponents.RAMs.Create(_itemBuilderService.BuildRAM(item));
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                _computerComponents.SataSSDs.Create(_itemBuilderService.BuildSataSSD(item));
                return;
            }
            // case ItemTypeDescriptor.PreparedAssembly: {
            //     return _itemBuilderService.BuildWrapper(
            //         _preparedAssemblyService.Get(id)
            //     );
            // }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }
    
    public void Update(ItemTypeDescriptor itemType, int id, AnonymousItemWrapper wrapped) {
        
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                _computerComponents.ComputerCases.Update(id, _itemBuilderService.BuildComputerCase(wrapped));
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                _computerComponents.CPUCoolers.Update(id, _itemBuilderService.BuildCPUCooler(wrapped));
                return;
            }
            case ItemTypeDescriptor.CPU: {
                _computerComponents.CPUs.Update(id, _itemBuilderService.BuildCPU(wrapped));
                return;
            }
            case ItemTypeDescriptor.Display: {
                _computerComponents.Displays.Update(id, _itemBuilderService.BuildDisplay(wrapped));
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                _computerComponents.GraphicsCards.Update(id, _itemBuilderService.BuildGraphicsCard(wrapped));
                return;
            }
            case ItemTypeDescriptor.HDD: {
                _computerComponents.HDDs.Update(id, _itemBuilderService.BuildHDD(wrapped));
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                _computerComponents.Keyboards.Update(id, _itemBuilderService.BuildKeyboard(wrapped));
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                _computerComponents.Motherboards.Update(id, _itemBuilderService.BuildMotherboard(wrapped));
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                _computerComponents.Mouses.Update(id, _itemBuilderService.BuildMouse(wrapped));
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                _computerComponents.NVMeSSDs.Update(id, _itemBuilderService.BuildNVMeSSD(wrapped));
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                _computerComponents.PowerSupplies.Update(id, _itemBuilderService.BuildPowerSupply(wrapped));
                return;
            }
            case ItemTypeDescriptor.RAM: {
                _computerComponents.RAMs.Update(id, _itemBuilderService.BuildRAM(wrapped));
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                _computerComponents.SataSSDs.Update(id, _itemBuilderService.BuildSataSSD(wrapped));
                return;
            }
            // case ItemTypeDescriptor.PreparedAssembly: {
            //     return _itemBuilderService.BuildWrapper(
            //         _preparedAssemblyService.Get(id)
            //     );
            // }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    public void Delete(ItemTypeDescriptor itemType, int id) {
        
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                _computerComponents.ComputerCases.Delete(id);
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                _computerComponents.CPUCoolers.Delete(id);
                return;
            }
            case ItemTypeDescriptor.CPU: {
                _computerComponents.CPUs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Display: {
                _computerComponents.Displays.Delete(id);
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                _computerComponents.GraphicsCards.Delete(id);
                return;
            }
            case ItemTypeDescriptor.HDD: {
                _computerComponents.HDDs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                _computerComponents.Keyboards.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                _computerComponents.Motherboards.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                _computerComponents.Mouses.Delete(id);
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                _computerComponents.NVMeSSDs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                _computerComponents.PowerSupplies.Delete(id);
                return;
            }
            case ItemTypeDescriptor.RAM: {
                _computerComponents.RAMs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                _computerComponents.SataSSDs.Delete(id);
                return;
            }
            // case ItemTypeDescriptor.PreparedAssembly: {
            //     return _itemBuilderService.BuildWrapper(
            //         _preparedAssemblyService.Get(id)
            //     );
            // }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    public void Save(ItemTypeDescriptor itemType) {
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                _computerComponents.ComputerCases.Save();
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                _computerComponents.CPUCoolers.Save();
                return;
            }
            case ItemTypeDescriptor.CPU: {
                _computerComponents.CPUs.Save();
                return;
            }
            case ItemTypeDescriptor.Display: {
                _computerComponents.Displays.Save();
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                _computerComponents.GraphicsCards.Save();
                return;
            }
            case ItemTypeDescriptor.HDD: {
                _computerComponents.HDDs.Save();
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                _computerComponents.Keyboards.Save();
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                _computerComponents.Motherboards.Save();
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                _computerComponents.Mouses.Save();
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                _computerComponents.NVMeSSDs.Save();
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                _computerComponents.PowerSupplies.Save();
                return;
            }
            case ItemTypeDescriptor.RAM: {
                _computerComponents.RAMs.Save();
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                _computerComponents.SataSSDs.Save();
                return;
            }
            // case ItemTypeDescriptor.PreparedAssembly: {
            //     return _itemBuilderService.BuildWrapper(
            //         _preparedAssemblyService.Get(id)
            //     );
            // }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }
    
}