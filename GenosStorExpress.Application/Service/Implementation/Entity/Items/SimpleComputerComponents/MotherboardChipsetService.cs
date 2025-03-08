using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class MotherboardChipsetService: AbstractSimpleComputerComponentService, IMotherboardChipsetService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IMotherboardChipsetRepository _motherboardChipsets;

        public MotherboardChipsetService(ISimpleComputerComponentTypeService simpleComputerComponentTypeService, IGenosStorExpressRepositories repositories) : base(simpleComputerComponentTypeService) {
            _repositories = repositories;
            _motherboardChipsets = _repositories.Items.SimpleComputerComponents.MotherboardChipsets;
        }
        
        public void Create(MotherboardChipsetWrapper item) {
            var created = new MotherboardChipset();
            _setEntityPropertiesFromWrapper(created, item);
            _motherboardChipsets.Create(created);
        }

        public MotherboardChipsetWrapper Get(int id) {
            MotherboardChipset obj = _motherboardChipsets.Get(id);
            var wrapped = new MotherboardChipsetWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<MotherboardChipsetWrapper> List() {
            return _motherboardChipsets.List().Select(obj => {
                var wrapped = new MotherboardChipsetWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, MotherboardChipsetWrapper item) {
            var obj = _motherboardChipsets.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            _motherboardChipsets.Create(obj);
        }

        public void Delete(int id) {
            _motherboardChipsets.Delete(id);
        }

        public MotherboardChipset GetRaw(int id) {
            return _motherboardChipsets.Get(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}