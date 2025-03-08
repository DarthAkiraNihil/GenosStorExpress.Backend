using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class NVMeSSDService: AbstractSSDService, INVMeSSDService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly INVMeSSDRepository _nvmeSSDs;

        public NVMeSSDService(IItemTypeService itemTypeService, IVendorService vendorService, ISSDControllerService ssdControllerService, IGenosStorExpressRepositories repositories) : base(itemTypeService, vendorService, ssdControllerService) {
            _repositories = repositories;
            _nvmeSSDs = _repositories.Items.ComputerComponents.NVMeSSDs;
        }
        
        public void Create(NVMeSSDWrapper item) {
            var created = new NVMeSSD();
            _setEntityPropertiesFromWrapper(created, item);
            _nvmeSSDs.Create(created);
        }

        public NVMeSSDWrapper Get(int id) {
            NVMeSSD obj = _nvmeSSDs.Get(id);
            var wrapped = new NVMeSSDWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<NVMeSSDWrapper> List() {
            return _nvmeSSDs.List().Select(obj => {
                var wrapped = new NVMeSSDWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, NVMeSSDWrapper item) {
            var obj = _nvmeSSDs.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            _nvmeSSDs.Update(obj);
        }

        public void Delete(int id) {
            _nvmeSSDs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<NVMeSSDWrapper> Filter(List<Func<NVMeSSDWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}