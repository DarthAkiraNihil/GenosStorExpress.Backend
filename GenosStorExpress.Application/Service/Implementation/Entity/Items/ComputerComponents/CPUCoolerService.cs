using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class CPUCoolerService: AbstractComputerComponentService, ICPUCoolerService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICPUCoolerRepository _cpuCoolers;
        private readonly ICoolerMaterialService _coolerMaterials;

        public CPUCoolerService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICoolerMaterialService coolerMaterials) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _coolerMaterials = coolerMaterials;
            _cpuCoolers = _repositories.Items.ComputerComponents.CPUCoolers;
        }
        
        public void Create(CPUCoolerWrapper item) {
            
            var created = new CPUCooler();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.MaxFanRPM = item.MaxFanRPM;
            created.TubesCount = item.TubesCount;
            created.TubesDiameter = item.TubesDiameter;
            created.FanCount = item.FanCount;
            created.FoundationMaterial = _coolerMaterials.GetEntityFromString(item.FoundationMaterial);
            created.RadiatorMaterial = _coolerMaterials.GetEntityFromString(item.RadiatorMaterial);
            
            _cpuCoolers.Create(created);
        }

        public CPUCoolerWrapper Get(int id) {
            
            CPUCooler obj = _cpuCoolers.Get(id);
            var wrapped = new CPUCoolerWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.MaxFanRPM = obj.MaxFanRPM;
            wrapped.TubesCount = obj.TubesCount;
            wrapped.TubesDiameter = obj.TubesDiameter;
            wrapped.FanCount = obj.FanCount;
            wrapped.RadiatorMaterial = obj.RadiatorMaterial.Name;
            wrapped.FoundationMaterial = obj.FoundationMaterial.Name;
            
            return wrapped;
        }

        public List<CPUCoolerWrapper> List() {
            return _cpuCoolers.List().Select(obj => {
                var wrapped = new CPUCoolerWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.MaxFanRPM = obj.MaxFanRPM;
                wrapped.TubesCount = obj.TubesCount;
                wrapped.TubesDiameter = obj.TubesDiameter;
                wrapped.FanCount = obj.FanCount;
                wrapped.RadiatorMaterial = obj.RadiatorMaterial.Name;
                wrapped.FoundationMaterial = obj.FoundationMaterial.Name;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, CPUCoolerWrapper item) {
            var obj = _cpuCoolers.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.MaxFanRPM = item.MaxFanRPM;
            obj.TubesCount = item.TubesCount;
            obj.TubesDiameter = item.TubesDiameter;
            obj.FanCount = item.FanCount;
            obj.FoundationMaterial = _coolerMaterials.GetEntityFromString(item.FoundationMaterial);
            obj.RadiatorMaterial = _coolerMaterials.GetEntityFromString(item.RadiatorMaterial);
            
            _cpuCoolers.Update(obj);
        }

        public void Delete(int id) {
            _cpuCoolers.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<CPUCoolerWrapper> Filter(List<Func<CPUCoolerWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}