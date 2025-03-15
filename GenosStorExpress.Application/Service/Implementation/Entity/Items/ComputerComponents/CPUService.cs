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

            var core = _cpuCoreService.GetRaw(item.Core.Id);
            if (core == null) {
                throw new NullReferenceException($"Ядра процессора с номером {item.Core.Id} ({item.Core.Name} не существует)");
            }
            created.Core = core;
            
            var socket = _cpusSocketService.GetEntityFromString(item.Socket);
            if (socket == null) {
                throw new NullReferenceException($"Сокета {item.Socket} не существует)");
            }
            created.Socket = socket;
            
            created.CoresCount = item.CoresCount;
            created.ThreadsCount = item.ThreadsCount;
            created.L2CahceSize = item.L2CacheSize;
            created.L3CacheSize = item.L3CacheSize;
            created.TechnicalProcess = item.TechnicalProcess;
            created.BaseFrequency = item.BaseFrequency;
            created.SupportedRamType = item.SupportedRamTypes.Select(i => {
                var type = _ramTypeService.GetEntityFromString(i);
                if (type == null) {
                    throw new NullReferenceException($"Типа ОЗУ {i} не существует)");
                }

                return type;
            }).ToList();
            created.SupportedRAMSize = item.SupportedRAMSize;
            created.HasIntegratedGraphics = item.HasIntegratedGraphics;
            
            _cpus.Create(created);
        }

        public CPUWrapper? Get(int id) {
            CPU? obj = _cpus.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new CPUWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.Core = new CPUCoreWrapper {Id = obj.Core.Id, Name = obj.Core.Name, Vendor = obj.Core.Vendor.Name};
            wrapped.Socket = obj.Socket.Name;
            wrapped.CoresCount = obj.CoresCount;
            wrapped.ThreadsCount = obj.ThreadsCount;
            wrapped.L2CacheSize = obj.L2CahceSize;
            wrapped.L3CacheSize = obj.L3CacheSize;
            wrapped.TechnicalProcess = obj.TechnicalProcess;
            wrapped.BaseFrequency = obj.BaseFrequency;
            wrapped.SupportedRamTypes = obj.SupportedRamType.Select(i => i.Name).ToList();
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
                wrapped.L2CacheSize = obj.L2CahceSize;
                wrapped.L3CacheSize = obj.L3CacheSize;
                wrapped.TechnicalProcess = obj.TechnicalProcess;
                wrapped.BaseFrequency = obj.BaseFrequency;
                wrapped.SupportedRamTypes = obj.SupportedRamType.Select(i => i.Name).ToList();
                wrapped.SupportedRAMSize = obj.SupportedRAMSize;
                wrapped.HasIntegratedGraphics = obj.HasIntegratedGraphics;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, CPUWrapper item) {
            
            var obj = _cpus.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Процессора с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            var core = _cpuCoreService.GetRaw(item.Core.Id);
            if (core == null) {
                throw new NullReferenceException($"Ядра процессора с номером {item.Core.Id} ({item.Core.Name} не существует)");
            }
            obj.Core = core;
            
            var socket = _cpusSocketService.GetEntityFromString(item.Socket);
            if (socket == null) {
                throw new NullReferenceException($"Сокета {item.Socket} не существует)");
            }
            obj.Socket = socket;

            obj.CoresCount = item.CoresCount;
            obj.ThreadsCount = item.ThreadsCount;
            obj.L2CahceSize = item.L2CacheSize;
            obj.L3CacheSize = item.L3CacheSize;
            obj.TechnicalProcess = item.TechnicalProcess;
            obj.BaseFrequency = item.BaseFrequency;
            obj.SupportedRamType = item.SupportedRamTypes.Select(i => {
                var type = _ramTypeService.GetEntityFromString(i);
                if (type == null) {
                    throw new NullReferenceException($"Типа ОЗУ {i} не существует)");
                }

                return type;
            }).ToList();
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