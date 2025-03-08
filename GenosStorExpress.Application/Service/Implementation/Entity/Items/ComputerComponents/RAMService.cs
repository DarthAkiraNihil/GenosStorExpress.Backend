using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class RAMService: AbstractComputerComponentService, IRAMService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IRAMRepository _rams;
        private readonly IRAMTypeService _ramTypeService;

        public RAMService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IRAMTypeService ramTypeService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _ramTypeService = ramTypeService;
            _rams = _repositories.Items.ComputerComponents.RAMs;
        }

        public void Create(RAMWrapper item) {
            var created = new RAM();
            
            _setEntityPropertiesFromWrapper(created, item);
            
            created.TotalSize = item.TotalSize;
            created.ModuleSize = item.ModuleSize;
            created.ModulesCount = item.ModulesCount;
            created.Frequency = item.Frequency;
            created.CL = item.CL;
            created.tRCD = item.tRCD;
            created.tRP = item.tRP;
            created.tRAS = item.tRAS;
            created.Type = _ramTypeService.GetEntityFromString(item.Type);
            
            _rams.Create(created);
        }

        public RAMWrapper Get(int id) {
            RAM obj = _rams.Get(id);
            var wrapped = new RAMWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.TotalSize = obj.TotalSize;
            wrapped.ModuleSize = obj.ModuleSize;
            wrapped.ModulesCount = obj.ModulesCount;
            wrapped.Frequency = obj.Frequency;
            wrapped.CL = obj.CL;
            wrapped.tRCD = obj.tRCD;
            wrapped.tRP = obj.tRP;
            wrapped.tRAS = obj.tRAS;
            wrapped.Type = obj.Type.Name;
            
            return wrapped;
        }

        public List<RAMWrapper> List() {
            return _rams.List().Select(obj => {
                var wrapped = new RAMWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.TotalSize = obj.TotalSize;
                wrapped.ModuleSize = obj.ModuleSize;
                wrapped.ModulesCount = obj.ModulesCount;
                wrapped.Frequency = obj.Frequency;
                wrapped.CL = obj.CL;
                wrapped.tRCD = obj.tRCD;
                wrapped.tRP = obj.tRP;
                wrapped.tRAS = obj.tRAS;
                wrapped.Type = obj.Type.Name;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, RAMWrapper item) {
            var obj = _rams.Get(id);
            
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.TotalSize = item.TotalSize;
            obj.ModuleSize = item.ModuleSize;
            obj.ModulesCount = item.ModulesCount;
            obj.Frequency = item.Frequency;
            obj.CL = item.CL;
            obj.tRCD = item.tRCD;
            obj.tRP = item.tRP;
            obj.tRAS = item.tRAS;
            obj.Type = _ramTypeService.GetEntityFromString(item.Type);
            
            _rams.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.RAMs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<RAMWrapper> Filter(List<Func<RAMWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}