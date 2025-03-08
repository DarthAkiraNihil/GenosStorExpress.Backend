using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class CPUService: AbstractComputerComponentService, ICPUService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICPURepository _cpus;
        private readonly ICPUCoreService _cpuCoreService;
        private readonly ICPUSocketService _cpusSocketService;
        private readonly IRAMTypeService _ramTypeService;

        public CPUService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICPUCoreService cpuCoreService, ICPUSocketService cpusSocketService, IRAMTypeService ramTypeService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _cpuCoreService = cpuCoreService;
            _cpusSocketService = cpusSocketService;
            _ramTypeService = ramTypeService;
            _cpus = _repositories.Items.ComputerComponents.CPUs;
        }

        public void Create(CPUWrapper item) {

            var created = new CPU();
            _setEntityPropertiesFromWrapper(created, item);

            created.Core = _cpuCoreService.GetRaw(item.Core.Id);
            created.Socket = _cpusSocketService.GetEntityFromString(item.Socket);
            created.CoresCount = item.CoresCount;
            created.ThreadsCount = item.ThreadsCount;
            created.L2CahceSize = item.L2CahceSize;
            created.L3CacheSize = item.L3CacheSize;
            created.TechnicalProcess = item.TechnicalProcess;
            created.BaseFrequency = item.BaseFrequency;
            created.SupportedRamType = item.SupportedRamType.Select(i => _ramTypeService.GetEntityFromString(i)).ToList();
            created.SupportedRAMSize = item.SupportedRAMSize;
            created.HasIntegratedGraphics = item.HasIntegratedGraphics;
            
            _cpus.Create(created);
        }

        public CPUWrapper Get(int id) {
            CPU obj = _cpus.Get(id);
            var wrapped = new CPUWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.Core = new CPUCoreWrapper {Id = obj.Core.Id, Name = obj.Core.Name, Vendor = obj.Core.Vendor.Name};
            wrapped.Socket = obj.Socket.Name;
            wrapped.CoresCount = obj.CoresCount;
            wrapped.ThreadsCount = obj.ThreadsCount;
            wrapped.L2CahceSize = obj.L2CahceSize;
            wrapped.L3CacheSize = obj.L3CacheSize;
            wrapped.TechnicalProcess = obj.TechnicalProcess;
            wrapped.BaseFrequency = obj.BaseFrequency;
            wrapped.SupportedRamType = obj.SupportedRamType.Select(i => i.Name).ToList();
            wrapped.SupportedRAMSize = obj.SupportedRAMSize;
            wrapped.HasIntegratedGraphics = obj.HasIntegratedGraphics;
            
            return wrapped;
        }

        public List<CPUWrapper> List() {
            return _cpus.List().Select(obj => {
                var wrapped = new CPUWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.Core = new CPUCoreWrapper {Id = obj.Core.Id, Name = obj.Core.Name, Vendor = obj.Core.Vendor.Name};
                wrapped.Socket = obj.Socket.Name;
                wrapped.CoresCount = obj.CoresCount;
                wrapped.ThreadsCount = obj.ThreadsCount;
                wrapped.L2CahceSize = obj.L2CahceSize;
                wrapped.L3CacheSize = obj.L3CacheSize;
                wrapped.TechnicalProcess = obj.TechnicalProcess;
                wrapped.BaseFrequency = obj.BaseFrequency;
                wrapped.SupportedRamType = obj.SupportedRamType.Select(i => i.Name).ToList();
                wrapped.SupportedRAMSize = obj.SupportedRAMSize;
                wrapped.HasIntegratedGraphics = obj.HasIntegratedGraphics;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, CPUWrapper item) {
            
            var obj = _cpus.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);

            obj.Core = _cpuCoreService.GetRaw(item.Core.Id);
            obj.Socket = _cpusSocketService.GetEntityFromString(item.Socket);
            obj.CoresCount = item.CoresCount;
            obj.ThreadsCount = item.ThreadsCount;
            obj.L2CahceSize = item.L2CahceSize;
            obj.L3CacheSize = item.L3CacheSize;
            obj.TechnicalProcess = item.TechnicalProcess;
            obj.BaseFrequency = item.BaseFrequency;
            obj.SupportedRamType = item.SupportedRamType.Select(i => _ramTypeService.GetEntityFromString(i)).ToList();
            obj.SupportedRAMSize = item.SupportedRAMSize;
            obj.HasIntegratedGraphics = item.HasIntegratedGraphics;
            
            _cpus.Update(obj);
        }

        public void Delete(int id) {
            _cpus.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<CPUWrapper> Filter(List<Func<CPUWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}