using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.User;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class PreparedAssemblyService: IPreparedAssemblyService {
        private readonly IGenosStorExpressRepositories _repositories;
        
        private readonly IPreparedAssemblyRepository _preparedAssemblies;
        private readonly IRAMRepository _rams;
        private readonly IDiskDriveRepository _diskDrives;
        private readonly ICPURepository _cpus;
        private readonly IMotherboardRepository _motherboards;
        private readonly IGraphicsCardRepository _graphicsCards;
        private readonly IPowerSupplyRepository _powerSupplies;
        private readonly IDisplayRepository _displays;
        private readonly IComputerCaseRepository _computerCases;
        private readonly IKeyboardRepository _keyboards;
        private readonly IMouseRepository _mouses;
        private readonly ICPUCoolerRepository _cpuCoolers;

        public PreparedAssemblyService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _rams = _repositories.Items.ComputerComponents.RAMs;
            _diskDrives = _repositories.Items.ComputerComponents.DiskDrives;
            _cpus = _repositories.Items.ComputerComponents.CPUs;
            _motherboards = _repositories.Items.ComputerComponents.Motherboards;
            _graphicsCards = _repositories.Items.ComputerComponents.GraphicsCards;
            _powerSupplies = _repositories.Items.ComputerComponents.PowerSupplies;
            _displays = _repositories.Items.ComputerComponents.Displays;
            _diskDrives = _repositories.Items.ComputerComponents.DiskDrives;
            _computerCases = _repositories.Items.ComputerComponents.ComputerCases;
            _keyboards = _repositories.Items.ComputerComponents.Keyboards;
            _mouses = _repositories.Items.ComputerComponents.Mouses;
            _cpuCoolers = _repositories.Items.ComputerComponents.CPUCoolers;
        }
        
        public void Create(PreparedAssemblyWrapper item) {
            var created = new PreparedAssembly {
                RAM = item.RAMs.Select(i => _rams.Get(i.Id)).ToList(),
                Disks = item.DiskDrives.Select(i => _diskDrives.Get(i.Id)).ToList(),
                CPU = _cpus.Get(item.CPU.Id),
                Motherboard = _motherboards.Get(item.Motherboard.Id),
                GraphicsCard = _graphicsCards.Get(item.GraphicsCard.Id),
                PowerSupply = _powerSupplies.Get(item.PowerSupply.Id),
                Display = _displays.Get(item.Display!.Id),
                ComputerCase = _computerCases.Get(item.ComputerCase.Id),
                Keyboard = _keyboards.Get(item.Keyboard.Id),
                Mouse = _mouses.Get(item.Mouse.Id),
                CPUCooler = _cpuCoolers.Get(item.CPUCooler.Id)
            };
            _preparedAssemblies.Create(created);
        }

        public PreparedAssemblyWrapper Get(int id) {
            PreparedAssembly obj = _preparedAssemblies.Get(id);
            return new PreparedAssemblyWrapper {
                RAMs = obj.RAM.Select(i => _wrap(i)).ToList(),
                DiskDrives = obj.Disks.Select(i => _wrap(i)).ToList(),
                CPU = _wrap(obj.CPU),
                Motherboard = _wrap(obj.Motherboard),
                GraphicsCard = _wrap(obj.GraphicsCard),
                PowerSupply = _wrap(obj.PowerSupply),
                Display = _wrap(obj.Display),
                ComputerCase = _wrap(obj.ComputerCase),
                Keyboard = _wrap(obj.Keyboard),
                Mouse = _wrap(obj.Mouse),
                CPUCooler = _wrap(obj.CPUCooler),
            };
        }

        public List<PreparedAssemblyWrapper> List() {
            return _preparedAssemblies.List().Select(obj => new PreparedAssemblyWrapper {
                RAMs = obj.RAM.Select(i => _wrap(i)).ToList(),
                DiskDrives = obj.Disks.Select(i => _wrap(i)).ToList(),
                CPU = _wrap(obj.CPU),
                Motherboard = _wrap(obj.Motherboard),
                GraphicsCard = _wrap(obj.GraphicsCard),
                PowerSupply = _wrap(obj.PowerSupply),
                Display = _wrap(obj.Display),
                ComputerCase = _wrap(obj.ComputerCase),
                Keyboard = _wrap(obj.Keyboard),
                Mouse = _wrap(obj.Mouse),
                CPUCooler = _wrap(obj.CPUCooler),
            }).ToList();
        }

        public void Update(int id, PreparedAssemblyWrapper item) {
            PreparedAssembly obj = _preparedAssemblies.Get(id);
            
            obj.RAM = item.RAMs.Select(i => _rams.Get(i.Id)).ToList();
            obj.Disks = item.DiskDrives.Select(i => _diskDrives.Get(i.Id)).ToList();
            obj.CPU = _cpus.Get(item.CPU.Id);
            obj.Motherboard = _motherboards.Get(item.Motherboard.Id);
            obj.GraphicsCard = _graphicsCards.Get(item.GraphicsCard.Id);
            obj.PowerSupply = _powerSupplies.Get(item.PowerSupply.Id);
            obj.Display = _displays.Get(item.Display!.Id);
            obj.ComputerCase = _computerCases.Get(item.ComputerCase.Id);
            obj.Keyboard = _keyboards.Get(item.Keyboard.Id);
            obj.Mouse = _mouses.Get(item.Mouse.Id);
            obj.CPUCooler = _cpuCoolers.Get(item.CPUCooler.Id);
            
            _preparedAssemblies.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.PreparedAssemblies.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        private PreparedAssemblyItemWrapper _wrap(Item wrapped) {
            return new PreparedAssemblyItemWrapper {
                Id = wrapped.Id, Model = wrapped.Model
            };
        }

        private PreparedAssemblyDiskDriveWrapper _wrap(DiskDrive wrapped) {
            return new PreparedAssemblyDiskDriveWrapper() {
                Id = wrapped.Id, Model = wrapped.Model, Type = wrapped.ItemType.Name
            };
        }
    }
}